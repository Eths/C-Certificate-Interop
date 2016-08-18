using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace CertificateDll
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual),
    Guid("41F13203-A603-4576-A206-FED2E1862991"), ComVisible(true)]
    interface ICertificate
    {
        X509Certificate2 SelectCertificate();
    }
}
