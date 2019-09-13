// This file contains your Data Connector logic
section ToDoApp.PowerBiConnector;

token_uri = "https://login.microsoftonline.com/tfp/b6110487-86e3-418f-aba6-7f26f3bccc48/B2C_1_SignUpSignIn/oauth2/v2.0/token";
authorize_uri = "https://login.microsoftonline.com/tfp/b6110487-86e3-418f-aba6-7f26f3bccc48/B2C_1_SignUpSignIn/oauth2/v2.0/authorize";
redirect_uri = "https://preview.powerbi.com/views/oauthredirect.html";

scope_prefix = "https://appdatadev.onmicrosoft.com/todo";
scopes = {
    "ReadAll"
};
windowWidth = 800;
windowHeight = 800;

client_id = Text.FromBinary(Extension.Contents("client_id"));
client_secret = Text.FromBinary(Extension.Contents("client_secret"));

BaseUrl = "https://jvm-todo-api.azurewebsites.net";
OAuthBaseUrl = "https://login.microsoftonline.com/tfp/b6110487-86e3-418f-aba6-7f26f3bccc48/B2C_1_SignUpSignIn";

[DataSource.Kind="ToDoApp.PowerBiConnector", Publish="ToDoApp.PowerBiConnector.Publish"]
shared ToDoApp.PowerBiConnector.Contents = (optional message as text) =>
    let
        credential = Extension.CurrentCredential(),
        token = credential[access_token],
        headers = [Authorization = "Bearer " & token],
        

       
        //token = if (credential[AuthenticationKind] = "Key") then credential[Key] else credential[access_token],        
       // headers = [ Authorization = "Bearer " & token ],
        navTable = OpenApi.Document(Web.Contents("http://jvm-todo-api.azurewebsites.net/swagger/v1/swagger.json"), [ Headers = headers, ManualCredentials = true ])
 
    
in
    navTable;


// Data Source Kind description
ToDoApp.PowerBiConnector = [
TestConnection = (dataSourcePath) => {"ToDoApp.PowerBiConnector.Contents", dataSourcePath},
    Authentication = [
    OAuth = [
            StartLogin = StartLogin,
            FinishLogin = FinishLogin,
            Refresh=Refresh
        ]
        // Key = [],
        // UsernamePassword = [],
        // Windows = [],
        //Implicit = []
    ],
    Label = Extension.LoadString("DataSourceLabel")
];

// Data Source UI publishing description
ToDoApp.PowerBiConnector.Publish = [
    Beta = true,
    Category = "Other",
    ButtonText = { Extension.LoadString("ButtonTitle"), Extension.LoadString("ButtonHelp") },
    LearnMoreUrl = "https://jvm-todo-web.azurewebsites.net/",
    SourceImage = ToDoApp.PowerBiConnector.Icons,
    SourceTypeImage = ToDoApp.PowerBiConnector.Icons
];

ToDoApp.PowerBiConnector.Icons = [
    Icon16 = { Extension.Contents("ToDoApp.PowerBiConnector16.png"), Extension.Contents("ToDoApp.PowerBiConnector20.png"), Extension.Contents("ToDoApp.PowerBiConnector24.png"), Extension.Contents("ToDoApp.PowerBiConnector32.png") },
    Icon32 = { Extension.Contents("ToDoApp.PowerBiConnector32.png"), Extension.Contents("ToDoApp.PowerBiConnector40.png"), Extension.Contents("ToDoApp.PowerBiConnector48.png"), Extension.Contents("ToDoApp.PowerBiConnector64.png") }
];

//
// OAuth2 flow definition
//

StartLogin = (resourceUrl, state, display) =>
    let
        authorizeUrl = authorize_uri & "?" & Uri.BuildQueryString([
            client_id = client_id,  
            redirect_uri = redirect_uri,
            state = state,
            scope = GetScopeString(scopes, scope_prefix) & " offline_access",
            response_type = "code",
            response_mode = "query",
            login = "login"
        ])
    in
        [
            LoginUri = authorizeUrl,
            CallbackUri = redirect_uri,
            WindowHeight = 720,
            WindowWidth = 1024,
            Context = null
        ];

FinishLogin = (context, callbackUri, state) =>
    let
        // parse the full callbackUri, and extract the Query string
        parts = Uri.Parts(callbackUri)[Query],
        // if the query string contains an "error" field, raise an error
        // otherwise call TokenMethod to exchange our code for an access_token
        result = if (Record.HasFields(parts, {"error", "error_description"})) then 
                    error Error.Record(parts[error], parts[error_description], parts)
                 else
                    TokenMethod("authorization_code", "code", parts[code])
    in
        result;

TokenMethod = (grantType, tokenField, code) =>
    let
        queryString = [
            client_id = client_id,
            client_secret = client_secret,
            scope = GetScopeString(scopes, scope_prefix) & " offline_access",
            grant_type = grantType,
            redirect_uri = redirect_uri
        ],
        queryWithCode = Record.AddField(queryString, tokenField, code),

        tokenResponse = Web.Contents(token_uri, [
            Content = Text.ToBinary(Uri.BuildQueryString(queryWithCode)),
            Headers = [
                #"Content-type" = "application/x-www-form-urlencoded",
                #"Accept" = "application/json"
            ],
            ManualStatusHandling = {400} 
        ]),
        body = Json.Document(tokenResponse),
        result = if (Record.HasFields(body, {"error", "error_description"})) then 
                    error Error.Record(body[error], body[error_description], body)
                 else
                    body
    in
        result;

Refresh = (resourceUrl, refresh_token) => TokenMethod("refresh_token", "refresh_token", refresh_token);


//
// Helper Functions
//
Value.IfNull = (a, b) => if a <> null then a else b;

GetScopeString = (scopes as list, optional scopePrefix as text) as text =>
    let
        prefix = Value.IfNull(scopePrefix, ""),
        addPrefix = List.Transform(scopes, each prefix & "/" & _),
        asText = Text.Combine(addPrefix, " ")
    in
        asText;
