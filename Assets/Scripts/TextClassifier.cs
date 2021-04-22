using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using IBM.Watson.NaturalLanguageClassifier.V1;
using IBM.Watson.NaturalLanguageClassifier.V1.Model;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK;

public class TextClassifier : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
        [Space(10)]
        [Tooltip("The IAM apikey.")]
        [SerializeField]
        private string iamApikey = "3Yf0hnWTv-UDdI2Y66WoTEe_fstj8xseDbFBUfBOX9oQ";
		[Tooltip("The service URL (optional). This defaults to \"https://api.us-south.natural-language-classifier.watson.cloud.ibm.com\"")]
        [SerializeField]
        private string serviceUrl = "https://api.us-south.natural-language-classifier.watson.cloud.ibm.com/instances/5efd0429-4da1-4d22-8ed1-78928296944f";
        
        #endregion

        private NaturalLanguageClassifierService service;
        private string nluText = "IBM is an American multinational technology company headquartered in Armonk, New York, United States with operations in over 170 countries.";

        public bool serviceInitiated = false;
        private bool IMBServicesConnection = false;
        private bool textClassificationConnection = false;
        public static Result result;
        public RoomASceneManager managerA;
        public RoomBSceneManager managerB;

    // Start is called before the first frame update
    void Start()
    {
        Runnable.Run(CreateService()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateService(){
        if(!IMBServicesConnection){
            Debug.Log("Running service initiater again");
            Runnable.Run(CreateService());
        }
    }

    public void InitiateTextClassification(){
        if(!textClassificationConnection){
            Debug.Log("Running text classification again");
            Runnable.Run(RunTextClassification());
        }
    }

    public void RunClassification(){
        Runnable.Run(RunTextClassification());
    }

    public void RunClassification(string classificationText){
        Runnable.Run(RunTextClassification(classificationText));
    }


    private IEnumerator CreateService()
        {
            if (string.IsNullOrEmpty(iamApikey))
            {
                throw new IBMException("Please add IAM ApiKey to the Iam Apikey field in the inspector.");
            }

            IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

            while (!authenticator.CanAuthenticate())
            {
                yield return null;
            }

			service = new NaturalLanguageClassifierService(authenticator);
            if (!string.IsNullOrEmpty(serviceUrl))
            {
                service.SetServiceUrl(serviceUrl);
                IMBServicesConnection = true;
                Debug.Log("Service initiated");
                Runnable.Run(RunTextClassification());
            }

            InitiateService();


            // Runnable.Run(ExampleListClassifiers());
            // Runnable.Run(RunTextClassification());
        }

    
    private IEnumerator RunTextClassification(){
        var authenticator = new IamAuthenticator(
            apikey: iamApikey
            );
        while (!authenticator.CanAuthenticate())
            yield return null;

        var naturalLanguageClassifier = new NaturalLanguageClassifierService(authenticator);
        naturalLanguageClassifier.SetServiceUrl(serviceUrl);
        Debug.Log("Natural language classifier initiated");


        Classification classifyResponse = null;
        service.Classify(
            callback: (DetailedResponse<Classification> response, IBMError error) =>
            {
                Log.Debug("NaturalLanguageClassifierServiceV1", "Classify result: {0}", response.Response);
                Debug.Log(response.Response);
                classifyResponse = response.Result;
                serviceInitiated = true;
                textClassificationConnection = true;
            },
            // classifierId: "5c9a68x914-nlc-306",
            classifierId: "5c9a68x914-nlc-465",
            text: "Who open the window?"
        );

        while (classifyResponse == null)
            yield return null;
        InitiateTextClassification();
        
    }

    private IEnumerator RunTextClassification(string classificationText){
        var authenticator = new IamAuthenticator(
            apikey: iamApikey
            );
        while (!authenticator.CanAuthenticate())
            yield return null;

        var naturalLanguageClassifier = new NaturalLanguageClassifierService(authenticator);
        naturalLanguageClassifier.SetServiceUrl(serviceUrl);
        Debug.Log("Sending " + classificationText);


        Classification classifyResponse = null;
        service.Classify(
            callback: (DetailedResponse<Classification> response, IBMError error) =>
            {
                Log.Debug("NaturalLanguageClassifierServiceV1", "Classify result: {0}", response.Response);
                Debug.Log(response.Response);
                classifyResponse = response.Result;
                result = JsonUtility.FromJson<Result>(response.Response);
                Debug.Log(result.top_class);
                if(SceneManager.GetActiveScene().name == "RoomA"){
                    managerA.result = result;
                    managerA.ProcessResult();
                }else if(SceneManager.GetActiveScene().name == "RoomB"){
                    managerB.result = result;
                    managerB.ProcessResult();
                }
                
            },
            classifierId: "5c9a68x914-nlc-465",
            text: classificationText + "?" 
        );

        while (classifyResponse == null)
            yield return null;
    }


}
