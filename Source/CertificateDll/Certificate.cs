using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace CertificateDll
{
    [ClassInterface(ClassInterfaceType.None), Guid("85E5EB5B-1C33-4F94-AA27-1E88107C9F52"), ComVisible(true), ProgId("Eths.CertificateDll")]
    public class Certificate : ICertificate
    {
        #region Certificate
        public X509Certificate2 SelectCertificate()
        {
            X509Store st = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            st.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection col = st.Certificates.Find(X509FindType.FindByKeyUsage,
                X509KeyUsageFlags.NonRepudiation, true);

            X509Certificate2 cert = null;
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(col,
                "Certificados", "Selecione um para assinar", X509SelectionFlag.SingleSelection);
            if (sel.Count > 0)
            {
                X509Certificate2Enumerator en = sel.GetEnumerator();
                en.MoveNext();
                cert = en.Current;
            }
            st.Close();


            if (cert == null)
                throw new InvalidOperationException("Certificado selecionado não é válido."); ;

            //Normalmente não consegue acessar no certificado A3, por que falta a digitação do PIN
            if (cert.PrivateKey == null)
                throw new Exception("Não foi possível acessar a chave privada do certificado digital.");

            return cert;
        }
        #endregion
    }
}
