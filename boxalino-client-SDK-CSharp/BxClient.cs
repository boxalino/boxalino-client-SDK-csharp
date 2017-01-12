using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Thrift.Collections;
using Thrift;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;
using boxalino_client_SDK_CSharp.Services;
using System.Net;
using boxalino_client_SDK_CSharp.Exception;

namespace boxalino_client_SDK_CSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class BxClient
    {
        private string _account;
        private string _password;
        private bool _isDev;
        private string _host;
        private int? _port;
        private string _uri;
        private string _schema;
        private string _p13n_username;
        private string _p13n_password;
        private string _domain;
        private List<BxAutocompleteRequest> _autocompleteRequests = null;
        private List<BxAutocompleteResponse> _autocompleteResponses = null;

        private List<BxRequest> _chooseRequests;
        private ChoiceResponse _chooseResponses = null;

        const int _VISITOR_COOKIE_TIME = 31536000;
        private int _timeout = 2;
        private Dictionary<string, List<string>> _requestContextParameters;

        private string _sessionId = null;
        private string _profileId = null;

        private Dictionary<string, string> _requestMap;


        public string account { get { return _account; } set { this._account = value; } }
        public string password { get { return _password; } set { this._password = value; } }
        public bool isDev { get { return _isDev; } set { this._isDev = value; } }
        public string host { get { return _host; } set { this._host = value; } }
        public int? port { get { return _port; } set { this._port = value; } }
        public string uri { get { return _uri; } set { this._uri = value; } }
        public string schema { get { return _schema; } set { this._schema = value; } }
        public string p13n_username { get { return _p13n_username; } set { this._p13n_username = value; } }
        public string p13n_password { get { return _p13n_password; } set { this._p13n_password = value; } }
        public string domain { get { return _domain; } set { this._domain = value; } }
        public List<BxAutocompleteRequest> autocompleteRequests { get { return _autocompleteRequests; } set { this._autocompleteRequests = value; } }
        public List<BxAutocompleteResponse> autocompleteResponses { get { return _autocompleteResponses; } set { this._autocompleteResponses = value; } }
        public List<BxRequest> chooseRequests { get { return _chooseRequests; } set { this._chooseRequests = value; } }
        public ChoiceResponse chooseResponses { get { return _chooseResponses; } set { this._chooseResponses = value; } }

        public Dictionary<string, List<string>> requestContextParameters { get { return _requestContextParameters; } set { this._requestContextParameters = value; } }

        public Dictionary<string, string> requestMap { get { return _requestMap; } set { this._requestMap = value; } }
        public string sessionId { get { return _sessionId; } set { this._sessionId = value; } }
        public string profileId { get { return _profileId; } set { this._profileId = value; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="BxClient"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="isDev">if set to <c>true</c> [is dev].</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="schema">The schema.</param>
        /// <param name="p13n_username">The P13N username.</param>
        /// <param name="p13n_password">The P13N password.</param>
        public BxClient(string account, string password, string domain, bool isDev = false, string host = null, int? port = null, string uri = null, string schema = null, string p13n_username = null, string p13n_password = null)
        {
            this.chooseRequests = new List<BxRequest>();
            this.requestMap = new Dictionary<string, string>();
            this._timeout = 2;
            this.requestContextParameters = new Dictionary<string, List<string>>();
            this.sessionId = null;
            this.profileId = null;



            this.account = account;
            this.password = password;
            this.requestMap = requestMap;
            this.isDev = isDev;

            this.host = host;
            if (this.host == null)
            {
                this.host = "cdn.bx-cloud.com";
            }
            this.port = port;
            if (this.port == null)
            {
                this.port = 443;
            }
            this.uri = uri;
            if (this.uri == null)
            {
                this.uri = "/p13n.web/p13n";
            }
            this.schema = schema;
            if (this.schema == null)
            {
                this.schema = "https";
            }
            this.p13n_username = p13n_username;
            if (this.p13n_username == null)
            {
                this.p13n_username = "boxalino";
            }
            this.p13n_password = p13n_password;
            if (this.p13n_password == null)
            {
                this.p13n_password = "tkZ8EXfzeZc6SdXZntCU";
            }
            this.domain = domain;
        }

        /// <summary>
        /// Sets the request map.
        /// </summary>
        /// <param name="requestMap">The request map.</param>
        public void setRequestMap(Dictionary<string, string> requestMap)
        {
            this.requestMap = requestMap;
        }



        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <param name="checkDev">if set to <c>true</c> [check dev].</param>
        /// <returns></returns>
        public string getAccount(bool checkDev = true)
        {
            if (checkDev && this.isDev)
            {
                return this.account + "_dev";
            }
            return this.account;
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <returns></returns>
        public string getUsername()
        {
            return this.getAccount(false);
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <returns></returns>
        public string getPassword()
        {
            return this.password;
        }

        /// <summary>
        /// Sets the session and profile.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        public void setSessionAndProfile(string sessionId, string profileId)
        {
            this.sessionId = sessionId;
            this.profileId = profileId;
        }

        /// <summary>
        /// Gets the session and profile.
        /// </summary>
        /// <returns></returns>
        private string[] getSessionAndProfile()
        {

            if (this.sessionId != null && this.profileId != null)
            {
                return new string[] { this.sessionId, this.profileId };
            }

            if (HttpContext.Current.Request.Cookies["cems"] == null)
            {
                sessionId = Common.session_id();
                if (String.IsNullOrEmpty(sessionId))
                {
                    Common.session_start();
                    sessionId = Common.session_id();
                }
            }
            else
            {
                sessionId = HttpContext.Current.Request.Cookies["cems"].Value;
            }

            if (HttpContext.Current.Request.Cookies["cemv"] == null)
            {
                profileId = Common.session_id();
                if (String.IsNullOrEmpty(profileId))
                {
                    Common.session_start();
                    profileId = Common.session_id();
                }
            }
            else
            {
                profileId = HttpContext.Current.Request.Cookies["cemv"].Value;
            }
            
            if (String.IsNullOrEmpty(this.domain))
            {
                HttpCookie cookie = new HttpCookie("cems", sessionId);
                cookie.Expires.AddSeconds(_VISITOR_COOKIE_TIME);
                Common.setcookie(cookie);

                HttpCookie cookie1 = new HttpCookie("cemv", profileId);
                cookie1.Expires.AddSeconds(_VISITOR_COOKIE_TIME);
            }
            else
            {
                HttpCookie cookie = new HttpCookie("cems", sessionId);
                cookie.Expires.AddDays(7);
                Common.setcookie(cookie);
                cookie.Path = "/";
                cookie.Domain = domain;
                HttpCookie cookie1 = new HttpCookie("cemv", profileId);
                cookie1.Expires.AddSeconds(31536000);
                cookie.Path = "/";
                cookie.Domain = domain;
                Common.setcookie(cookie1);
            }

            this.sessionId = sessionId;
            this.profileId = profileId;

            return new String[] { this.sessionId, this.profileId };
        }

        /// <summary>
        /// Gets the user record.
        /// </summary>
        /// <returns></returns>
        private boxalino_client_SDK_CSharp.Services.UserRecord getUserRecord()
        {
            boxalino_client_SDK_CSharp.Services.UserRecord userRecord = new boxalino_client_SDK_CSharp.Services.UserRecord();
           
            userRecord.Username = this.getAccount();
            return userRecord;
        }

        /// <summary>
        /// Gets the P13N.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <param name="useCurlIfAvailable">if set to <c>true</c> [use curl if available].</param>
        /// <returns></returns>
        private P13nService.Client getP13n(int timeout = 2, bool useCurlIfAvailable = true)
        {
            useCurlIfAvailable = false;
            THttpClient transport = null;
            if (useCurlIfAvailable)
            {
               
            }
            else
            {
                transport = new Thrift.Transport.THttpClient(new Uri(String.Format("{0}://{1}{2}", this.schema, this.host, this.uri), UriKind.Absolute));
            }
            transport.setAuthorization(this.p13n_username, this.p13n_password);
          


            P13nService.Client client = new P13nService.Client(new Thrift.Protocol.TCompactProtocol(transport));
            transport.Open();
            return client;
        }

        /// <summary>
        /// Gets the request context parameters.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> getRequestContextParameters()
        {
            Dictionary<string, List<string>> parameters = this.requestContextParameters;
            foreach (var request in chooseRequests)
            {
                dynamic child = request;
                foreach (var v in child.getRequestContextParameters())
                {
                    parameters[v.Key] = v.Value;
                }
            }
            return parameters;
        }

        /// <summary>
        /// Gets the request context.
        /// </summary>
        /// <returns></returns>
        protected RequestContext getRequestContext()
        {
            string[] list = this.getSessionAndProfile();
            RequestContext requestContext = new RequestContext();           
            requestContext.Parameters = new Dictionary<string, List<string>>
        {
            { "User-Agent", new List<string>(){HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]??string.Empty  }},
            {"User-Host",new List<string>(){this.getIP()??string.Empty}} ,
            {"User-SessionId",new List<string>(){this.sessionId}},
            {"User-Referer",new List<string>(){ Convert.ToString(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]==null? this.getCurrentURL(): HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]) }}, //
            {"User-URL",new List<string>(){this.getCurrentURL()}}
        };
            foreach (var k in this.getRequestContextParameters())
            {
                requestContext.Parameters[k.Key] = k.Value;
            }
            if (this.requestMap != null && requestMap.ContainsKey("p13nRequestContext"))
            {
                requestContext.Parameters =
                new Dictionary<string, List<string>> { { this.requestMap["p13nRequestContext"], requestContext.Parameters["p13nRequestContext"] } };

            }
            return requestContext;
        }
        /// <summary>
        /// Gets the choice request.
        /// </summary>
        /// <param name="inquiries">The inquiries.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        public ChoiceRequest getChoiceRequest(List<ChoiceInquiry> inquiries, RequestContext requestContext = null)
        {
            ChoiceRequest choiceRequest = new ChoiceRequest();
            String[] list = this.getSessionAndProfile();

            choiceRequest.UserRecord = this.getUserRecord();
            choiceRequest.ProfileId = profileId;
            choiceRequest.Inquiries = inquiries;
            if (requestContext == null)
            {
                requestContext = this.getRequestContext();
            }
            choiceRequest.RequestContext = requestContext;
            return choiceRequest;
        }

        /// <summary>
        /// Gets the ip.
        /// </summary>
        /// <returns></returns>
        protected string getIP()
        {
            string ip = null;
            string clientip = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            string forwardedip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            IPAddress address;
            if (IPAddress.TryParse(clientip, out address))
            {
                ip = clientip;
            }
            else if (IPAddress.TryParse(forwardedip, out address))
            {
                ip = forwardedip;
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        /// <summary>
        /// Gets the current URL.
        /// </summary>
        /// <returns></returns>
        protected string getCurrentURL()
        {
            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PROTOCOL"]==null?string.Empty:(HttpContext.Current.Request.ServerVariables["SERVER_PROTOCOL"].ToLower().Contains("https") ? "http" : "https");
            string hostname = HttpContext.Current.Request.ServerVariables["HTTP_HOST"]==null?string.Empty: HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            string requesturi = HttpContext.Current.Request.ServerVariables["REQUEST_URI"]==null?string.Empty: HttpContext.Current.Request.ServerVariables["REQUEST_URI"];
            return protocol + "://" + hostname + requesturi;
        }

        /// <summary>
        /// Adds the request context parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void addRequestContextParameter(string name, List<string> value)
        {

            this.requestContextParameters.Add(name, value);
        }

        /// <summary>
        /// Resets the request context parameter.
        /// </summary>
        public void resetRequestContextParameter()
        {
            this.requestContextParameters = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Gets the basic request context parameters.
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, List<string>> getBasicRequestContextParameters()
        {
            sessionId = this.getSessionAndProfile()[0];
            profileId = this.getSessionAndProfile()[1];

            return new Dictionary<string, List<string>>
            {
                { "User-Agent", new List<string>(){HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]   }},
                {"User-Host",new List<string>(){this.getIP()}} ,
                {"User-SessionId",new List<string>(){this.sessionId}},
                {"User-Referer",new List<string>(){ HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]}},
                {"User-URL",new List<string>(){this.getCurrentURL()}}
            };

        }


        /// <summary>
        /// Throws the correct P13N exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <exception cref="BoxalinoException">
        /// The connection to our server failed even before checking your credentials. This might be typically caused by 2 possible things: wrong values in host, port, schema or uri (typical value should be host=cdn.bx-cloud.com, port=443, uri =/p13n.web/p13n and schema=https, your values are : host=' . " + this.host + ", port=" + this.port + ", schema=" + this.schema + ", uri=" + this.uri + "). Another possibility, is that your server environment has a problem with ssl certificate (peer certificate cannot be authenticated with given ca certificates). Full error message= " + e.getMessage()
        /// or
        /// The connection to our server has worked, but your credentials were refused. Provided credentials username=" + this.p13n_username + ", password=" + this.p13n_password + ". Full error message=" + e.getMessage()
        /// or
        /// Configuration not live on account " + this.getAccount() + ": choice " + choiceId + " doesn't exist. NB: If you get a message indicating that the choice doesn't exist, go to http://intelligence.bx-cloud.com, log in your account and make sure that the choice id you want to use is published.
        /// or
        /// Data not live on account " + this.getAccount() + ": index returns status 404. Please publish your data first, like in example backend_data_basic.php.
        /// or
        /// You request in your filter or facets a non-existing field of your account " + this.getAccount() + ": field " + field + " doesn't exist.
        /// or
        /// You have an invalid configuration for with a choice defined, but having no defined strategies. This is a quite unusual case, please contact support@boxalino.com to get support.
        /// </exception>
        private void throwCorrectP13nException(BoxalinoException e)
        {
            if (e.getMessage().IndexOf("Could not connect ") <= 0)
            {
                throw new BoxalinoException("The connection to our server failed even before checking your credentials. This might be typically caused by 2 possible things: wrong values in host, port, schema or uri (typical value should be host=cdn.bx-cloud.com, port=443, uri =/p13n.web/p13n and schema=https, your values are : host=' . " + this.host + ", port=" + this.port + ", schema=" + this.schema + ", uri=" + this.uri + "). Another possibility, is that your server environment has a problem with ssl certificate (peer certificate cannot be authenticated with given ca certificates). Full error message= " + e.getMessage());
            }

            if (e.getMessage().IndexOf("Bad protocol id in TCompact message") <= 0)
            {
                throw new BoxalinoException("The connection to our server has worked, but your credentials were refused. Provided credentials username=" + this.p13n_username + ", password=" + this.p13n_password + ". Full error message=" + e.getMessage());
            }

            if (e.getMessage().IndexOf("choice not found") <= 0)
            {
                string[] parts = e.getMessage().Split(new string[] { "choice not found" }, StringSplitOptions.None);
                string[] pieces = parts[1].Split(new string[] { "at" }, StringSplitOptions.None);
                string choiceId = pieces[0].Replace(':', ' ');
                throw new BoxalinoException("Configuration not live on account " + this.getAccount() + ": choice " + choiceId + " doesn't exist. NB: If you get a message indicating that the choice doesn't exist, go to http://intelligence.bx-cloud.com, log in your account and make sure that the choice id you want to use is published.");
            }

            if (e.getMessage().IndexOf("Solr returned status 404") <= 0)
            {
                throw new BoxalinoException("Data not live on account " + this.getAccount() + ": index returns status 404. Please publish your data first, like in example backend_data_basic.php.");

            }

            if (e.getMessage().IndexOf("undefined field") <= 0)
            {
                string[] parts = e.getMessage().Split(new string[] { "undefined field" }, StringSplitOptions.None);
                string[] pieces = parts[1].Split(new string[] { "at" }, StringSplitOptions.None);
                string field = pieces[0].Replace(':', ' ');

                throw new BoxalinoException("You request in your filter or facets a non-existing field of your account " + this.getAccount() + ": field " + field + " doesn't exist.");
            }

            if (e.getMessage().IndexOf("All choice variants are excluded") <= 0)
            {
                throw new BoxalinoException("You have an invalid configuration for with a choice defined, but having no defined strategies. This is a quite unusual case, please contact support@boxalino.com to get support.");
            }

            throw e;
        }
        /// <summary>
        /// P13nchooses the specified choice request.
        /// </summary>
        /// <param name="choiceRequest">The choice request.</param>
        /// <returns></returns>
        private ChoiceResponse p13nchoose(ChoiceRequest choiceRequest)
        {
            try
            {
                ChoiceResponse choiceResponse = this.getP13n(this._timeout).choose(choiceRequest);
                if (this.requestMap.Count > 0 && this.requestMap["dev_bx_disp"] != null && this.requestMap["dev_bx_disp"] == "true")
                {

                    System.Web.HttpContext.Current.Response.Write("<pre><h1>Choice Request</h1>");
                    System.Web.HttpContext.Current.Response.Write(Convert.ToString(choiceRequest.GetType()));
                    System.Web.HttpContext.Current.Response.Write("Inquiries" + choiceRequest.Inquiries);
                    System.Web.HttpContext.Current.Response.Write("ProfileId" + choiceRequest.ProfileId);
                    System.Web.HttpContext.Current.Response.Write("RequestContext" + choiceRequest.RequestContext);
                    System.Web.HttpContext.Current.Response.Write("UserRecord" + choiceRequest.UserRecord);

                    System.Web.HttpContext.Current.Response.Write("<br><h1>Choice Response</h1>");
                    System.Web.HttpContext.Current.Response.Write(Convert.ToString(choiceResponse.GetType()));
                    System.Web.HttpContext.Current.Response.Write("Variants" + choiceResponse.Variants);
                    System.Web.HttpContext.Current.Response.Write("</pre>");
                }
                return choiceResponse;
            }
            catch (BoxalinoException e)
            {
                throw e;
                
            }
        }

        /// <summary>
        /// Adds the request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void addRequest(BxRequest request)
        {
            request.setDefaultIndexId(this.getAccount());
            request.setDefaultRequestMap(this.requestMap);
            this.chooseRequests.Add(request);
        }

        /// <summary>
        /// Resets the requests.
        /// </summary>
        public void resetRequests()
        {
            this.chooseRequests = new List<BxRequest>();
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public BxRequest getRequest(int index = 0)
        {
            if (this.chooseRequests.Count <= index)
            {
                return null;
            }
            return this.chooseRequests[index];
        }
        /// <summary>
        /// Gets the choice identifier recommendation request.
        /// </summary>
        /// <param name="choiceId">The choice identifier.</param>
        /// <returns></returns>
        public BxRequest getChoiceIdRecommendationRequest(string choiceId)
        {
            foreach (var request in chooseRequests)
            {
                if (request.getChoiceId() == choiceId)
                {
                    return request;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets the recommendation requests.
        /// </summary>
        /// <returns></returns>
        public List<BxRequest> getRecommendationRequests()
        {
            List<BxRequest> requests = new List<BxRequest>();

            foreach (var request in chooseRequests)
            {
                if (request.GetType() == typeof(BxRecommendationRequest))
                {
                    requests.Add(request);
                }
            }
            return requests;
        }

        /// <summary>
        /// Gets the thrift choice request.
        /// </summary>
        /// <returns></returns>
        public ChoiceRequest getThriftChoiceRequest()
        {
            List<ChoiceInquiry> choiceInquiries = new List<ChoiceInquiry>();

            foreach (BxRequest request in this.chooseRequests)
            {
                ChoiceInquiry choiceInquiry = new ChoiceInquiry();
                choiceInquiry.ChoiceId = request.getChoiceId();
                choiceInquiry.SimpleSearchQuery = request.getSimpleSearchQuery(); 

                choiceInquiry.ContextItems = request.getContextItems();
                choiceInquiry.MinHitCount = (int)request.getMin();
                choiceInquiry.WithRelaxation = request.getWithRelaxation();

                choiceInquiries.Add(choiceInquiry);
            }
            ChoiceRequest choiceRequest = this.getChoiceRequest(choiceInquiries, this.getRequestContext());
            return choiceRequest;
       }

        /// <summary>
        /// Chooses this instance.
        /// </summary>
        protected void choose()
        {
            this.chooseResponses = this.p13nchoose(this.getThriftChoiceRequest());
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns></returns>
        public BxChooseResponse getResponse()
        {
            if (this.chooseResponses == null)
            {
                this.choose();
            }
            return new BxChooseResponse(this.chooseResponses, this.chooseRequests);
        }

        /// <summary>
        /// Sets the autocomplete request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void setAutocompleteRequest(List<BxAutocompleteRequest> request)
        {
            this.setAutocompleteRequests(request);
        }

        /// <summary>
        /// Sets the autocomplete requests.
        /// </summary>
        /// <param name="requests">The requests.</param>
        public void setAutocompleteRequests(List<BxAutocompleteRequest> requests)
        {
            foreach (BxAutocompleteRequest request in requests)
            {
                this.enhanceAutoCompleterequest(request);
            }
            this.autocompleteRequests = requests;
        }

        /// <summary>
        /// Enhances the automatic completerequest.
        /// </summary>
        /// <param name="request">The request.</param>
        private void enhanceAutoCompleterequest(BxAutocompleteRequest request)
        {
            request.setDefaultIndexId(this.getAccount());
        }

        /// <summary>
        /// P13nautocompletes the specified autocomplete request.
        /// </summary>
        /// <param name="autocompleteRequest">The autocomplete request.</param>
        /// <returns></returns>
        private AutocompleteResponse p13nautocomplete(AutocompleteRequest autocompleteRequest)
        {
            try
            {
                return this.getP13n(this._timeout).autocomplete(autocompleteRequest);
            }
            catch (BoxalinoException e)
            {
                throw e; 
            }
        }
        /// <summary>
        /// Gets the autocomplete responses.
        /// </summary>
        /// <returns></returns>
        public List<BxAutocompleteResponse> getAutocompleteResponses()
        {
            if (this.autocompleteResponses == null)
            {
                this.autocomplete();
            }
            return this.autocompleteResponses;
        }

        /// <summary>
        /// Gets the autocomplete response.
        /// </summary>
        /// <returns></returns>
        public BxAutocompleteResponse getAutocompleteResponse()
        {
            List<BxAutocompleteResponse> responses = this.getAutocompleteResponses();
            if (responses[0] != null)
            {
                return responses[0];
            }
            return null;
        }
        /// <summary>
        /// Gets the bx autocomplete response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public BxAutocompleteResponse getBxAutocompleteResponse(AutocompleteResponse response, int i)
        {
            BxAutocompleteRequest request = this.autocompleteRequests[++i];
            return new BxAutocompleteResponse(response, request);
        }
        /// <summary>
        /// Autocompletes this instance.
        /// </summary>
        public void autocomplete()
        {
            string[] str = this.getSessionAndProfile();
            sessionId = str[0];
            profileId = str[1];

            UserRecord userRecord = this.getUserRecord();

            List<AutocompleteRequest> p13nrequests = use(this.autocompleteRequests, profileId, userRecord);
            int i = -1;
            this.autocompleteResponses = use(this.p13nautocompleteAll(p13nrequests), i);


        }

        /// <summary>
        /// Uses the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="userRecord">The user record.</param>
        /// <returns></returns>
        public List<AutocompleteRequest> use(dynamic request, string profileId, UserRecord userRecord)
        {
            List<AutocompleteRequest> listAutocompleteRequest = new List<AutocompleteRequest>();
            foreach(var req in request)
            {
                listAutocompleteRequest.Add(req.getAutocompleteThriftRequest(profileId, userRecord));
            }          
            return listAutocompleteRequest;
        }
        /// <summary>
        /// Uses the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public List<BxAutocompleteResponse> use(dynamic response, int i)
        {
            BxAutocompleteRequest request = this.autocompleteRequests[++i];
            List<BxAutocompleteResponse> bxAutocompleteResponse = new List<BxAutocompleteResponse>();
            foreach (var req in response)
            {
                bxAutocompleteResponse.Add(new BxAutocompleteResponse((AutocompleteResponse)req, request));
            }
            return bxAutocompleteResponse;

        }

        /// <summary>
        /// P13nautocompletes all.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        private List<AutocompleteResponse> p13nautocompleteAll(List<AutocompleteRequest> requests)
        {
            AutocompleteRequestBundle requestBundle = new AutocompleteRequestBundle();
            requestBundle.Requests = requests;
            try
            {
               return this.getP13n(this._timeout).autocompleteAll(requestBundle).Responses;
            }
            catch (BoxalinoException e)
            {
                this.throwCorrectP13nException(e);
            }
            return null;
        }


        /// <summary>
        /// Sets the timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public void setTimeout(int timeout)
        {
            this._timeout = timeout;
        }
    }
}
