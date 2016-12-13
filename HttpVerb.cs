using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWatch
{
    // Enum HTTP_VERB 
    // Description: Http verbs defined in http.h
    // See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa364664(v=vs.85).aspx
    public enum HttpVerb
    {
        Unparsed,       // HttpVerbUnparsed,
        Unknown,        // HttpVerbUnknown,
        Invalid,        // HttpVerbInvalid,
        Options,        // HttpVerbOPTIONS
        Get,            // HttpVerbGET
        Head,           // HttpVerbHEAD
        Post,           // HttpVerbPOST
        Put,            // HttpVerbPUT   
        Delete,         // HttpVerbDELETE
        Trace,          // HttpVerbTRACE,
        Connect,        // HttpVerbCONNECT,
        Track,          // HttpVerbTRACK, used by Microsoft Cluster Server for a non-logged trace
        Move,           // HttpVerbMOVE,
        Copy,           // HttpVerbCOPY,
        PropFind,       // HttpVerbPROPFIND, WebDav
        PropPatch,      // HttpVerbPROPPATCH, WebDav
        MakeCollection, // HttpVerbMKCOL, WebDav create collection
        Lock,           // HttpVerbLOCK,
        Unlock,         // HttpVerbUNLOCK,
        Search,         // HttpVerbSEARCH,
        Maximum         // HttpVerbMaximum
    };
}
