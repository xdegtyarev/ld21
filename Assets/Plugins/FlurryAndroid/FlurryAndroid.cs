using UnityEngine;
using System.Collections;
using System.Collections.Generic;



#if UNITY_ANDROID
public enum FlurryAdPlacement
{
	BannerBottom,
	BannerTop,
	FullScreen
}



public class FlurryAndroid
{
	// store the FlurryAgent class so we can make calls easily
	private static AndroidJavaClass _flurryAgent;
	private static AndroidJavaObject _plugin;
		
	
	static FlurryAndroid()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_flurryAgent = new AndroidJavaClass( "com.flurry.android.FlurryAgent" );
	
		// find the plugin instance
		using( var pluginClass = new AndroidJavaClass( "com.prime31.FlurryPlugin" ) )
			_plugin = pluginClass.CallStatic<AndroidJavaObject>( "instance" );
	}
	
	
	public static string getAndroidId()
	{
		if( Application.platform != RuntimePlatform.Android )
			return string.Empty;
		
		return _plugin.Call<string>( "getAndroidId" );
	}

	
	#region Analytics
	
	// Starts up your Flurry session.  Call on application startup. Optionally initializes Flurry Ads and enables test ads.
	public static void onStartSession( string apiKey, bool initializeAds, bool enableTestAds )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "onStartSession", apiKey, initializeAds, enableTestAds );
	}
	

	// Ends the current session.  You MUST call this for the data to be sent off to Flurry's servers!
	public static void onEndSession()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		_plugin.Call( "onEndSession" );
	}
	
	
	// Changes the window during which a session can be resumed.  Must be called before onStartSession!
	public static void setContinueSessionMillis( long milliseconds )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_flurryAgent.CallStatic( "setContinueSessionMillis", milliseconds );
	}


	// Logs an event to Flurry that is optionally timed
	public static void logEvent( string eventName )
	{
		logEvent( eventName, false );
	}
	
	
	public static void logEvent( string eventName, bool isTimed )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		if( isTimed )
			_plugin.Call( "logTimedEvent", eventName );
		else
			_plugin.Call( "logEvent", eventName );
	}
	

	// Logs an event with optional key/value pairs that is optionally timed
	public static void logEvent( string eventName, Dictionary<string,string> parameters )
	{
		logEvent( eventName, parameters, false );
	}
	
	
	public static void logEvent( string eventName, Dictionary<string,string> parameters, bool isTimed )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		// create a hashMap and stick our dictionary values in there
		using( var hashMap = new AndroidJavaObject( "java.util.HashMap" ) )
		{
			// cache the "put" method
			var putMethod = AndroidJNIHelper.GetMethodID( hashMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;" );
			
			var arguments = new object[2];
			foreach( var entry in parameters )
			{
				using( var key = new AndroidJavaObject( "java.lang.String", entry.Key ) )
				{
					using( var val = new AndroidJavaObject( "java.lang.String", entry.Value ) )
					{
						arguments[0] = key;
						arguments[1] = val;
						AndroidJNI.CallObjectMethod( hashMap.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray( arguments ) );
					}
				} // end using
			} // end foreach
	
			if( isTimed )
				_plugin.Call( "logTimedEventWithParams", eventName, hashMap );
			else
				_plugin.Call( "logEventWithParams", eventName, hashMap );
		}
	}
	
	
	// Ends a timed event
	public static void endTimedEvent( string eventName )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "endTimedEvent", eventName );
	}
	

	// Use onPageView to report page view count
	public static void onPageView()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
			
		_flurryAgent.CallStatic( "onPageView" );
	}


	// Use onError to report application errors
	public static void onError( string errorId, string message, string errorClass )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_flurryAgent.CallStatic( "onError", errorId, message, errorClass );
	}
	

	// Use this to log the user's assigned ID or username in your system
	public static void setUserID( string userId )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
			
		_flurryAgent.CallStatic( "setUserId", userId );
	}


	// Use this to log the user's age. Valid inputs are between 1 and 109.
	public static void setAge( int age )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
			
		_flurryAgent.CallStatic( "setAge", age );
	}


	// To enable/disable FlurryAgent logging call
	public static void setLogEnabled( bool enable )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
			
		_flurryAgent.CallStatic( "setLogEnabled", enable );
	}
	
	#endregion
	
	
	#region Flurry Ads
	
	// Fetches an ad for the given space
	public static void fetchAdsForSpace( string adSpace, FlurryAdPlacement adSize )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		_plugin.Call( "fetchAdsForSpace", adSpace, (int)adSize );
	}
	

	// Attempts to display an ad if available. Timeout is the maximum time in milliseconds that Flurry should take to load the ad
	public static void displayAd( string adSpace, FlurryAdPlacement adSize, long timeout )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		_plugin.Call( "displayAd", adSpace, (int)adSize, timeout );
	}


	// Removes the ad from view
	public static void removeAd( string adSpace )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		_plugin.Call( "removeAd", adSpace );
	}


	// Checks to see if an ad is available. 
	public static void checkIfAdIsAvailable( string adSpace, FlurryAdPlacement adSize, long timeout )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		_plugin.Call( "isAdAvailable", adSpace, (int)adSize, timeout );
	}
	
	#endregion
	
}
#endif
