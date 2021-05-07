namespace Namespace {
    
    using websocket;
    
    using datetime = datetime.datetime;
    
    using json;
    
    using ssl;
    
    using time;
    
    using sys;
    
    using System.Collections.Generic;
    
    public static class Module {
        
        public static object QUERY_HEADSET_ID = 1;
        
        public static object CONNECT_HEADSET_ID = 2;
        
        public static object REQUEST_ACCESS_ID = 3;
        
        public static object AUTHORIZE_ID = 4;
        
        public static object CREATE_SESSION_ID = 5;
        
        public static object SUB_REQUEST_ID = 6;
        
        public static object SETUP_PROFILE_ID = 7;
        
        public static object QUERY_PROFILE_ID = 8;
        
        public static object TRAINING_ID = 9;
        
        public static object DISCONNECT_HEADSET_ID = 10;
        
        public static object CREATE_RECORD_REQUEST_ID = 11;
        
        public static object STOP_RECORD_REQUEST_ID = 12;
        
        public static object EXPORT_RECORD_ID = 13;
        
        public static object INJECT_MARKER_REQUEST_ID = 14;
        
        public static object SENSITIVITY_REQUEST_ID = 15;
        
        public static object MENTAL_COMMAND_ACTIVE_ACTION_ID = 16;
        
        public static object MENTAL_COMMAND_BRAIN_MAP_ID = 17;
        
        public static object MENTAL_COMMAND_TRAINING_THRESHOLD = 18;
        
        public class Cortex {
            
            public Cortex(object user, object debug_mode = false) {
                var url = "wss://localhost:6868";
                this.ws = websocket.create_connection(url, sslopt: new Dictionary<object, object> {
                    {
                        "cert_reqs",
                        ssl.CERT_NONE}});
                this.user = user;
                this.debug = debug_mode;
            }
            
            public virtual object query_headset() {
                Console.WriteLine("query headset --------------------------------");
                var query_headset_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        QUERY_HEADSET_ID},
                    {
                        "method",
                        "queryHeadsets"},
                    {
                        "params",
                        new Dictionary<object, object> {
                        }}};
                this.ws.send(json.dumps(query_headset_request, indent: 4));
                var result = this.ws.recv();
                Console.WriteLine(result);
                var result_dic = json.loads(result);
                this.headset_id = result_dic["result"][0]["id"];
                if (this.debug) {
                    // print('query headset result', json.dumps(result_dic, indent=4))            
                    Console.WriteLine(this.headset_id);
                }
            }
            
            public virtual object connect_headset() {
                Console.WriteLine("connect headset --------------------------------");
                var connect_headset_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        CONNECT_HEADSET_ID},
                    {
                        "method",
                        "controlDevice"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "command",
                                "connect"},
                            {
                                "headset",
                                this.headset_id}}}};
                this.ws.send(json.dumps(connect_headset_request, indent: 4));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("connect headset result", json.dumps(result_dic, indent: 4));
                }
            }
            
            public virtual object request_access() {
                Console.WriteLine("request access --------------------------------");
                var request_access_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "requestAccess"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "clientId",
                                this.user["client_id"]},
                            {
                                "clientSecret",
                                this.user["client_secret"]}}},
                    {
                        "id",
                        REQUEST_ACCESS_ID}};
                this.ws.send(json.dumps(request_access_request, indent: 4));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                }
            }
            
            public virtual object authorize() {
                Console.WriteLine("authorize --------------------------------");
                var authorize_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "authorize"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "clientId",
                                this.user["client_id"]},
                            {
                                "clientSecret",
                                this.user["client_secret"]},
                            {
                                "license",
                                this.user["license"]},
                            {
                                "debit",
                                this.user["debit"]}}},
                    {
                        "id",
                        AUTHORIZE_ID}};
                if (this.debug) {
                    Console.WriteLine("auth request \n", json.dumps(authorize_request, indent: 4));
                }
                this.ws.send(json.dumps(authorize_request));
                while (true) {
                    var result = this.ws.recv();
                    var result_dic = json.loads(result);
                    if (result_dic.Contains("id")) {
                        if (result_dic["id"] == AUTHORIZE_ID) {
                            if (this.debug) {
                                Console.WriteLine("auth result \n", json.dumps(result_dic, indent: 4));
                            }
                            this.auth = result_dic["result"]["cortexToken"];
                            break;
                        }
                    }
                }
            }
            
            public virtual object create_session(object auth, object headset_id) {
                Console.WriteLine("create session --------------------------------");
                var create_session_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        CREATE_SESSION_ID},
                    {
                        "method",
                        "createSession"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "headset",
                                this.headset_id},
                            {
                                "status",
                                "active"}}}};
                if (this.debug) {
                    Console.WriteLine("create session request \n", json.dumps(create_session_request, indent: 4));
                }
                this.ws.send(json.dumps(create_session_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("create session result \n", json.dumps(result_dic, indent: 4));
                }
                this.session_id = result_dic["result"]["id"];
            }
            
            public virtual object close_session() {
                Console.WriteLine("close session --------------------------------");
                var close_session_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        CREATE_SESSION_ID},
                    {
                        "method",
                        "updateSession"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "session",
                                this.session_id},
                            {
                                "status",
                                "close"}}}};
                this.ws.send(json.dumps(close_session_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("close session result \n", json.dumps(result_dic, indent: 4));
                }
            }
            
            public virtual object get_cortex_info() {
                Console.WriteLine("get cortex version --------------------------------");
                var get_cortex_info_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "getCortexInfo"},
                    {
                        "id",
                        100}};
                this.ws.send(json.dumps(get_cortex_info_request));
                var result = this.ws.recv();
                if (this.debug) {
                    Console.WriteLine(json.dumps(json.loads(result), indent: 4));
                }
            }
            
            public virtual object do_prepare_steps() {
                this.query_headset();
                this.connect_headset();
                this.request_access();
                this.authorize();
                this.create_session(this.auth, this.headset_id);
            }
            
            public virtual object disconnect_headset() {
                Console.WriteLine("disconnect headset --------------------------------");
                var disconnect_headset_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        DISCONNECT_HEADSET_ID},
                    {
                        "method",
                        "controlDevice"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "command",
                                "disconnect"},
                            {
                                "headset",
                                this.headset_id}}}};
                this.ws.send(json.dumps(disconnect_headset_request));
                // wait until disconnect completed
                while (true) {
                    time.sleep(1);
                    var result = this.ws.recv();
                    var result_dic = json.loads(result);
                    if (this.debug) {
                        Console.WriteLine("disconnect headset result", json.dumps(result_dic, indent: 4));
                    }
                    if (result_dic.Contains("warning")) {
                        if (result_dic["warning"]["code"] == 1) {
                            break;
                        }
                    }
                }
            }
            
            public virtual object sub_request(object stream) {
                object new_data;
                Console.WriteLine("subscribe request --------------------------------");
                var sub_request_json = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "subscribe"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "session",
                                this.session_id},
                            {
                                "streams",
                                stream}}},
                    {
                        "id",
                        SUB_REQUEST_ID}};
                this.ws.send(json.dumps(sub_request_json));
                if (stream.Contains("sys")) {
                    new_data = this.ws.recv();
                    Console.WriteLine(json.dumps(new_data, indent: 4));
                    Console.WriteLine("\n");
                } else {
                    while (true) {
                        new_data = this.ws.recv();
                        Console.WriteLine(new_data);
                    }
                }
            }
            
            public virtual object query_profile() {
                Console.WriteLine("query profile --------------------------------");
                var query_profile_json = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "queryProfile"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth}}},
                    {
                        "id",
                        QUERY_PROFILE_ID}};
                if (this.debug) {
                    Console.WriteLine("query profile request \n", json.dumps(query_profile_json, indent: 4));
                    Console.WriteLine("\n");
                }
                this.ws.send(json.dumps(query_profile_json));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                Console.WriteLine("query profile result\n", result_dic);
                Console.WriteLine("\n");
                var profiles = new List<object>();
                foreach (var p in result_dic["result"]) {
                    profiles.append(p["name"]);
                }
                Console.WriteLine("extract profiles name only");
                Console.WriteLine(profiles);
                Console.WriteLine("\n");
                return profiles;
            }
            
            public virtual object setup_profile(object profile_name, object status) {
                Console.WriteLine("setup profile --------------------------------");
                var setup_profile_json = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "setupProfile"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "headset",
                                this.headset_id},
                            {
                                "profile",
                                profile_name},
                            {
                                "status",
                                status}}},
                    {
                        "id",
                        SETUP_PROFILE_ID}};
                if (this.debug) {
                    Console.WriteLine("setup profile json:\n", json.dumps(setup_profile_json, indent: 4));
                    Console.WriteLine("\n");
                }
                this.ws.send(json.dumps(setup_profile_json));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("result \n", json.dumps(result_dic, indent: 4));
                    Console.WriteLine("\n");
                }
            }
            
            public virtual object train_request(object detection, object action, object status) {
                object wanted_result;
                object accept_wanted_result;
                object start_wanted_result;
                // print('train request --------------------------------')
                var train_request_json = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "training"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "detection",
                                detection},
                            {
                                "session",
                                this.session_id},
                            {
                                "action",
                                action},
                            {
                                "status",
                                status}}},
                    {
                        "id",
                        TRAINING_ID}};
                // print('training request:\n', json.dumps(train_request_json, indent=4))
                // print('\n')
                this.ws.send(json.dumps(train_request_json));
                if (detection == "mentalCommand") {
                    start_wanted_result = "MC_Succeeded";
                    accept_wanted_result = "MC_Completed";
                }
                if (detection == "facialExpression") {
                    start_wanted_result = "FE_Succeeded";
                    accept_wanted_result = "FE_Completed";
                }
                if (status == "start") {
                    wanted_result = start_wanted_result;
                    Console.WriteLine("\n YOU HAVE 8 SECONDS FOR TRAIN ACTION {} \n".format(action.upper()));
                }
                if (status == "accept") {
                    wanted_result = accept_wanted_result;
                }
                // wait until success
                while (true) {
                    var result = this.ws.recv();
                    var result_dic = json.loads(result);
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                    if (result_dic.Contains("sys")) {
                        // success or complete, break the wait
                        if (result_dic["sys"][1] == wanted_result) {
                            break;
                        }
                    }
                }
            }
            
            public virtual object create_record(object record_name, object record_description) {
                Console.WriteLine("create record --------------------------------");
                var create_record_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "createRecord"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "session",
                                this.session_id},
                            {
                                "title",
                                record_name},
                            {
                                "description",
                                record_description}}},
                    {
                        "id",
                        CREATE_RECORD_REQUEST_ID}};
                this.ws.send(json.dumps(create_record_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("start record request \n", json.dumps(create_record_request, indent: 4));
                    Console.WriteLine("start record result \n", json.dumps(result_dic, indent: 4));
                }
                this.record_id = result_dic["result"]["record"]["uuid"];
            }
            
            public virtual object stop_record() {
                Console.WriteLine("stop record --------------------------------");
                var stop_record_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "stopRecord"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "session",
                                this.session_id}}},
                    {
                        "id",
                        STOP_RECORD_REQUEST_ID}};
                this.ws.send(json.dumps(stop_record_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("stop request \n", json.dumps(stop_record_request, indent: 4));
                    Console.WriteLine("stop result \n", json.dumps(result_dic, indent: 4));
                }
            }
            
            public virtual object export_record(
                object folder,
                object export_types,
                object export_format,
                object export_version,
                object record_ids) {
                Console.WriteLine("export record --------------------------------");
                var export_record_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        EXPORT_RECORD_ID},
                    {
                        "method",
                        "exportRecord"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "folder",
                                folder},
                            {
                                "format",
                                export_format},
                            {
                                "streamTypes",
                                export_types},
                            {
                                "recordIds",
                                record_ids}}}};
                // "version": export_version,
                if (export_format == "CSV") {
                    export_record_request["params"]["version"] = export_version;
                }
                if (this.debug) {
                    Console.WriteLine("export record request \n", json.dumps(export_record_request, indent: 4));
                }
                this.ws.send(json.dumps(export_record_request));
                // wait until export record completed
                while (true) {
                    time.sleep(1);
                    var result = this.ws.recv();
                    var result_dic = json.loads(result);
                    if (this.debug) {
                        Console.WriteLine("export record result \n", json.dumps(result_dic, indent: 4));
                    }
                    if (result_dic.Contains("result")) {
                        if (result_dic["result"]["success"].Count > 0) {
                            break;
                        }
                    }
                }
            }
            
            public virtual object inject_marker_request(object marker) {
                Console.WriteLine("inject marker --------------------------------");
                var inject_marker_request = new Dictionary<object, object> {
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "id",
                        INJECT_MARKER_REQUEST_ID},
                    {
                        "method",
                        "injectMarker"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "session",
                                this.session_id},
                            {
                                "label",
                                marker["label"]},
                            {
                                "value",
                                marker["value"]},
                            {
                                "port",
                                marker["port"]},
                            {
                                "time",
                                marker["time"]}}}};
                this.ws.send(json.dumps(inject_marker_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine("inject marker request \n", json.dumps(inject_marker_request, indent: 4));
                    Console.WriteLine("inject marker result \n", json.dumps(result_dic, indent: 4));
                }
            }
            
            public virtual object get_mental_command_action_sensitivity(object profile_name) {
                Console.WriteLine("get mental command sensitivity ------------------");
                var sensitivity_request = new Dictionary<object, object> {
                    {
                        "id",
                        SENSITIVITY_REQUEST_ID},
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "mentalCommandActionSensitivity"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "profile",
                                profile_name},
                            {
                                "status",
                                "get"}}}};
                this.ws.send(json.dumps(sensitivity_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                }
                return result_dic;
            }
            
            public virtual object set_mental_command_action_sensitivity(object profile_name, object values) {
                Console.WriteLine("set mental command sensitivity ------------------");
                var sensitivity_request = new Dictionary<object, object> {
                    {
                        "id",
                        SENSITIVITY_REQUEST_ID},
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "mentalCommandActionSensitivity"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "profile",
                                profile_name},
                            {
                                "session",
                                this.session_id},
                            {
                                "status",
                                "set"},
                            {
                                "values",
                                values}}}};
                this.ws.send(json.dumps(sensitivity_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                }
                return result_dic;
            }
            
            public virtual object get_mental_command_active_action(object profile_name) {
                Console.WriteLine("get mental command active action ------------------");
                var command_active_request = new Dictionary<object, object> {
                    {
                        "id",
                        MENTAL_COMMAND_ACTIVE_ACTION_ID},
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "mentalCommandActiveAction"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "profile",
                                profile_name},
                            {
                                "status",
                                "get"}}}};
                this.ws.send(json.dumps(command_active_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                }
                return result_dic;
            }
            
            public virtual object get_mental_command_brain_map(object profile_name) {
                Console.WriteLine("get mental command brain map ------------------");
                var brain_map_request = new Dictionary<object, object> {
                    {
                        "id",
                        MENTAL_COMMAND_BRAIN_MAP_ID},
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "mentalCommandBrainMap"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "profile",
                                profile_name},
                            {
                                "session",
                                this.session_id}}}};
                this.ws.send(json.dumps(brain_map_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                }
                return result_dic;
            }
            
            public virtual object get_mental_command_training_threshold(object profile_name) {
                Console.WriteLine("get mental command training threshold -------------");
                var training_threshold_request = new Dictionary<object, object> {
                    {
                        "id",
                        MENTAL_COMMAND_TRAINING_THRESHOLD},
                    {
                        "jsonrpc",
                        "2.0"},
                    {
                        "method",
                        "mentalCommandTrainingThreshold"},
                    {
                        "params",
                        new Dictionary<object, object> {
                            {
                                "cortexToken",
                                this.auth},
                            {
                                "session",
                                this.session_id}}}};
                this.ws.send(json.dumps(training_threshold_request));
                var result = this.ws.recv();
                var result_dic = json.loads(result);
                if (this.debug) {
                    Console.WriteLine(json.dumps(result_dic, indent: 4));
                }
                return result_dic;
            }
        }
    }
}
