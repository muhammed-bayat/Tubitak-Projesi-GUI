namespace Namespace {
    
    using Cortex = cortex.Cortex;
    
    using time;
    
    using System;
    
    using System.Linq;
    
    using System.Collections.Generic;
    
    public static class Module {
        
        public class Marker {
            
            public Marker() {
                this.c = Cortex(user, debug_mode: true);
                this.c.do_prepare_steps();
            }
            
            public virtual object add_markers(object marker_numbers) {
                foreach (var m in Enumerable.Range(0, marker_numbers)) {
                    var marker_time = time.time() * 1000;
                    Console.WriteLine("add marker at : ", marker_time);
                    var marker = new Dictionary<object, object> {
                        {
                            "label",
                            m.ToString()},
                        {
                            "value",
                            "test_marker"},
                        {
                            "port",
                            "python-app"},
                        {
                            "time",
                            marker_time}};
                    this.c.inject_marker_request(marker);
                    // add marker each seconds
                    time.sleep(3);
                }
            }
            
            public virtual object demo_add_marker(object record_export_folder, object marker_numbers) {
                // create record
                var record_name = "Marker video";
                var record_description = "test";
                this.c.create_record(record_name, record_description);
                this.add_markers(marker_numbers);
                this.c.stop_record();
                this.c.disconnect_headset();
                // export record
                var record_export_data_types = new List<object> {
                    "EEG",
                    "MOTION",
                    "PM",
                    "BP"
                };
                var record_export_format = "CSV";
                var record_export_version = "V2";
                this.c.export_record(record_export_folder, record_export_data_types, record_export_format, record_export_version, new List<object> {
                    this.c.record_id
                });
            }
        }
        
        public static object user = new Dictionary<object, object> {
            {
                "license",
                ""},
            {
                "client_id",
                ""},
            {
                "client_secret",
                ""},
            {
                "debit",
                100}};
        
        public static object m = Marker();
        
        public static object record_export_folder = "your place to export, you should have write permission, example on desktop";
        
        public static object marker_numbers = 10;
        
        static Module() {
            m.demo_add_marker(record_export_folder, marker_numbers);
        }
    }
}
