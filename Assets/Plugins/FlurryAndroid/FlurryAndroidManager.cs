using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;


public class FlurryAndroidManager : AbstractManager
{
	// Fired when an ad is available for a space
	public static event Action<string> adAvailableForSpaceEvent;

	// Fired when an ad is not available for a space
	public static event Action<string> adNotAvailableForSpaceEvent;

	// Fired when an ad is closed
	public static event Action<string> onAdClosedEvent;

	// Fired when an ad touch exits the application
	public static event Action<string> onApplicationExitEvent;

	// Fired when an ad render fails
	public static event Action<string> onRenderFailedEvent;

	// Fired when an ad space fails to receive an ad
	public static event Action<string> spaceDidFailToReceiveAdEvent;

	// Fired when an ad space receives an ad
	public static event Action<string> spaceDidReceiveAdEvent;

	
	
	static FlurryAndroidManager()
	{
		AbstractManager.initialize( typeof( FlurryAndroidManager ) );
	}


	public void adAvailableForSpace( string adSpace )
	{
		if( adAvailableForSpaceEvent != null )
			adAvailableForSpaceEvent( adSpace );
	}


	public void adNotAvailableForSpace( string adSpace )
	{
		if( adNotAvailableForSpaceEvent != null )
			adNotAvailableForSpaceEvent( adSpace );
	}


	public void onAdClosed( string adSpace )
	{
		if( onAdClosedEvent != null )
			onAdClosedEvent( adSpace );
	}


	public void onApplicationExit( string adSpace )
	{
		if( onApplicationExitEvent != null )
			onApplicationExitEvent( adSpace );
	}


	public void onRenderFailed( string adSpace )
	{
		if( onRenderFailedEvent != null )
			onRenderFailedEvent( adSpace );
	}


	public void spaceDidFailToReceiveAd( string adSpace )
	{
		if( spaceDidFailToReceiveAdEvent != null )
			spaceDidFailToReceiveAdEvent( adSpace );
	}


	public void spaceDidReceiveAd( string adSpace )
	{
		if( spaceDidReceiveAdEvent != null )
			spaceDidReceiveAdEvent( adSpace );
	}

}

