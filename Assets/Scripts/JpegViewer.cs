using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JpegViewer : MonoBehaviour {
   private const float FRAME_PERIOD = (float)0.0166666;

   public GameObject Background;       // The sphere on which we project
   public GameObject ControlsDisplay;
   public AudioSource audio;           // Video audio source
   public float frameRate = 30;        // Framerate of the source video
   public int numberOfFrames = 1462;   // Total number of frames
   public float rwd_modifier = (float)1.0;    // Both scaling modifiers for rwd/ffwd speed
   public float ffwd_modifier = (float)1.0;

   /* for keeping track of progress in video */
   private float time_elapsed;
   private float prev_time;
   private float end_time;

   /* Whether the video is paused or not */
   private bool playOn;

   /* Variable for debouncing controller button */
   private bool pressedLastTime_One;

   /* Indicates the start and end states */
   private bool atStart;
   private bool atEnd;

   /* buffer for video frames */
   private Texture2D[] frames;

   /* Store of the original starting texture of background sphere */
   private Texture origTexture;

   void Start () {
      /* Load xbox control display for start of scene */
      atStart = true; 
      atEnd = false;
      
      /* Save the original texture for later */ 
      origTexture = Background.GetComponent<Renderer>().material.mainTexture;

      /* load the frames */
      frames = new Texture2D[numberOfFrames];
      for (int i = 0; i < numberOfFrames; ++i)
         frames[i] = (Texture2D)Resources.Load(string.Format("frame{0:d4}", i + 1));

      playOn = false;
      pressedLastTime_One = false;

      time_elapsed = 0;
      prev_time = Time.time;
      end_time = numberOfFrames / frameRate;
   }

   void Update () {
      float delta_time = Time.time - prev_time; // time between this and last update
      bool rewindOn = false;
      bool fforwardOn = false;

      /* Collect left and right trigger inputs, 0 - 1.0 input range. Treating it as
       * a binary input for now */
      float primTrig, secTrig;
      primTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger); 
      secTrig = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

      /* Process the button inputs. Pause is on a single-press basis, rewind and fast
       * forward act based on how long they are pressed (so actions are taken every 
       * update for them) */ 
      if (debouncePressedButton_One()) {
         atStart = false; 
         playOn = !playOn;
      }
      if (secTrig > 0)
         fforwardOn = true;
      if (primTrig > 0)
         rewindOn = true;

      /* Apply rewind / fast forward effects */ 
      if (rewindOn) {
         /* Subtracting from time_elapsed effectively rewinds the frame */
         time_elapsed -= FRAME_PERIOD * rwd_modifier; 
         time_elapsed = (time_elapsed > 0) ? time_elapsed : 0;
         audio.Pause();
      }
      else if (fforwardOn) {
         /* Adding to time_elapsed effectively fast-forwards the frame */
         time_elapsed += FRAME_PERIOD * ffwd_modifier;
         time_elapsed = (time_elapsed < end_time) ? time_elapsed : end_time;
         audio.Pause();
      }
      else if (OVRInput.Get(OVRInput.Button.Four)) {
         Debug.Log("Pressed Exit");
         Application.LoadLevel("MenuScreen");
      }
      /* Apply whether it is playing or not. This means:
       * 1. Adding to time_elapsed based on the real-world time
       * 2. Enabling audio */
      if (playOn && !atEnd) {
         time_elapsed += delta_time; 
         time_elapsed = (time_elapsed < end_time) ? time_elapsed : end_time;

         /* Don't play audio on ffwd / rwd */
         if (!audio.isPlaying && !rewindOn && !fforwardOn) { 
           /* end_time can be slightly higher of a value than the actual length of audio
            * clip due to data imprecision, so this is just to make sure it doesn't seek
            * beyond the actual length and throw an error */
            audio.time = (time_elapsed >= audio.clip.length) ? audio.clip.length : time_elapsed; 

            audio.Play ();
         }
      }
      else {
         if (audio.isPlaying)
            audio.Pause();
      }

      /* Render out the current frame to the sphere */
      int currentFrame = (int)(time_elapsed * frameRate);
      if (currentFrame >= frames.Length) {
         Background.GetComponent<Renderer>().material.mainTexture = origTexture;
         ControlsDisplay.SetActive(true);
         atEnd = true;
         currentFrame = 0; 
      }

      /* Give option to return to main menu at the start or end */
/*      if (atEnd || atStart) {
         if (OVRInput.Get(OVRInput.Button.Four))
            Application.LoadLevel("MenuScreen");
      }*/
      
      if (atEnd) {
         if (OVRInput.Get(OVRInput.Button.One)) {
            atEnd = false;
            time_elapsed = 0;
         }
      }

      if (!atStart && !atEnd) {
         ControlsDisplay.SetActive(false);
         Background.GetComponent<Renderer>().material.mainTexture = frames[currentFrame];         
      }
      
      prev_time = Time.time;
   }

   /* "Debounces" a button press, so that multiple presses are not registered for a single press.
    * Returns true if it's a legit button press, false otherwise */
   bool debouncePressedButton_One () {
      bool returnVal = false; //default to no press
      bool isPressed = OVRInput.Get(OVRInput.Button.One);

      if (isPressed && !pressedLastTime_One)
         returnVal = true;

      pressedLastTime_One = isPressed; 

      return returnVal;
   }
}
