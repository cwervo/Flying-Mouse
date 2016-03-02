﻿using UnityEngine;
using System.Collections;


public class BirdModel : MonoBehaviour
{
    private float clock;		// Keep track of time since creation for animation.
    private Bird owner;			// Pointer to the parent object.

	public Material mat;		// Material for setting/changing texture and color.
	private AudioSource birdAudio;
	private AudioClip birdClip;
	public TrailRenderer birdTrail;


	float lifetime = 6;
	Rect clock_rect = new Rect(Screen.width - 150, 10, 150, 50);

	void OnGUI ()
	{
		if (!owner.playback) {
			GUI.Box (clock_rect, "Bird time: " + lifetime);
		}
	}

    public void init(Bird owner) {
        this.owner = owner;

        transform.parent = owner.transform;					// Set the model's parent to the bird.
        transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
        name = "Bird Model";									// Name the object.

        mat = GetComponent<Renderer>().material;								// Get the material component of this quad object.
//        mat.mainTexture = Resources.Load<Texture2D>("Textures/marble");	// Set the texture.  Must be in Resources folder.
        mat.mainTexture = Resources.Load<Texture2D>("Textures/bird");	// Set the texture.  Must be in Resources folder.
        mat.color = new Color(1,1,1);											// Set the color (easy way to tint things).
        mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency.

		// Audio stuff
		birdAudio = GetComponent<AudioSource> ();
		birdClip = GetComponent<AudioClip> ();
		birdAudio.Play ();

		// Trail Stuff
		birdTrail = this.gameObject.GetComponent<TrailRenderer> ();

    }

    void Start () {
        clock = 0f;
    }

    void Update () {
        // Incrememnt the clock based on how much time has elapsed since the previous update.
        // Using deltaTime is critical for animation and movement, since the time between each call
        // to Update is unpredictable.
        clock = clock + Time.deltaTime;
//
//		if (lifetime <= 0){
//			birdTrail.Clear ();
//		}
		if (lifetime <= 0 && !owner.playback) {
			// TODO: Add to gamemanager's list of repeatable birds
			owner.playback = true;
			owner.gm.birdOnScreen = false;
			owner.gm.dead_bird_list.Add (owner.name, owner);
//			Debug.Log (owner.gm.dead_bird_list.Count);
			//birdTrail.Clear ();
			print(birdTrail + " IS DED");
			//birdTrail.time = -1;
			birdTrail.Clear ();
			//Destroy (birdTrail);
			//birdTrail.receiveShadows = false;
			Destroy (this.gameObject);
		} else if (lifetime > 0) {
			lifetime -= Time.deltaTime;
		}
    }
}

