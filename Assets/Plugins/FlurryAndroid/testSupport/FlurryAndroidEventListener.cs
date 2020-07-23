using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class FlurryAndroidEventListener : MonoBehaviour
{

	void OnEnable()
	{
		// Listen to all events for illustration purposes
		FlurryAndroidManager.adAvailableForSpaceEvent += adAvailableForSpaceEvent;
		FlurryAndroidManager.adNotAvailableForSpaceEvent += adNotAvailableForSpaceEvent;
		FlurryAndroidManager.onAdClosedEvent += onAdClosedEvent;
		FlurryAndroidManager.onApplicationExitEvent += onApplicationExitEvent;
		FlurryAndroidManager.onRenderFailedEvent += onRenderFailedEvent;
		FlurryAndroidManager.spaceDidFailToReceiveAdEvent += spaceDidFailToReceiveAdEvent;
		FlurryAndroidManager.spaceDidReceiveAdEvent += spaceDidReceiveAdEvent;
	}


	void OnDisable()
	{
		// Remove all event handlers
		FlurryAndroidManager.adAvailableForSpaceEvent -= adAvailableForSpaceEvent;
		FlurryAndroidManager.adNotAvailableForSpaceEvent -= adNotAvailableForSpaceEvent;
		FlurryAndroidManager.onAdClosedEvent -= onAdClosedEvent;
		FlurryAndroidManager.onApplicationExitEvent -= onApplicationExitEvent;
		FlurryAndroidManager.onRenderFailedEvent -= onRenderFailedEvent;
		FlurryAndroidManager.spaceDidFailToReceiveAdEvent -= spaceDidFailToReceiveAdEvent;
		FlurryAndroidManager.spaceDidReceiveAdEvent -= spaceDidReceiveAdEvent;
	}



	void adAvailableForSpaceEvent( string adSpace )
	{
		Debug.Log( "adAvailableForSpaceEvent: " + adSpace );
	}


	void adNotAvailableForSpaceEvent( string adSpace )
	{
		Debug.Log( "adNotAvailableForSpaceEvent: " + adSpace );
	}


	void onAdClosedEvent( string adSpace )
	{
		Debug.Log( "onAdClosedEvent: " + adSpace );
	}


	void onApplicationExitEvent( string adSpace )
	{
		Debug.Log( "onApplicationExitEvent: " + adSpace );
	}


	void onRenderFailedEvent( string adSpace )
	{
		Debug.Log( "onRenderFailedEvent: " + adSpace );
	}


	void spaceDidFailToReceiveAdEvent( string adSpace )
	{
		Debug.Log( "spaceDidFailToReceiveAdEvent: " + adSpace );
	}


	void spaceDidReceiveAdEvent( string adSpace )
	{
		Debug.Log( "spaceDidReceiveAdEvent: " + adSpace );
	}


}


