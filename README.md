# Push Mate - FCM Push Service

[![Build status](https://ci.appveyor.com/api/projects/status/2r1l6ye1l1cj5rfp/branch/master?svg=true)](https://ci.appveyor.com/project/denstorti/pushmate-fcmpushservice/branch/master)

Allows you to send notifications using Firebase Cloud Messaging.

# How to use it

You need to have your own FCM Server Key to use this library. (Learn more here https://firebase.google.com/docs/cloud-messaging/server)

Create a Message instance:
 ```csharp
var message = new Message
            {
                RegistrationIds = new List<string> { registrationId },
                Notification = new Notification
                {
                    Title = "Title",
                    Body = $"Hello World@!{DateTime.Now.ToString()}"
                }
            };
```
  

Or simply use a JSON with the correct schema (https://firebase.google.com/docs/cloud-messaging/concept-options):
 ```csharp
string validJson = "{ \"registration_ids\":[\"dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym\"],\"priority\":\"Normal\",\"notification\":{\"title\":\"Title\",\"body\":\"Hello World@!23-Jun-18 19:58:47\"}}";
 ```
 Example of JSON request object:
 ```javascript
 {
  "message":{
    "token":"bk3RNwTe3H0:CI2k_HHwgIpoDKCIZvvDMExUdFQ3P1...",
    "notification":{
      "title":"Portugal vs. Denmark",
      "body":"great match!"
    },
    "data" : {
      "Nick" : "Mario",
      "Room" : "PortugalVSDenmark"
    }
  }
}
 ```
 
 Create an instance of the service FcmPushService:
 ```csharp
var fcmPushService = new FcmPushService(serverKey, null, new MockedHttpClientOK());
 ```

Call the async method for sending a push notification (for now it only supports HTTPS; XMPP on the road map);
```csharp
var result = await fcmPushService.SendAsync(validJson);
```

