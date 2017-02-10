//
// This code was written by Keith Brown, and may be freely used.
// Want to learn more about .NET? Visit pluralsight.com today!
//
using System;
using Pluralsight.Crypto.UI;
using System.Windows.Forms;
using Pluralsight.Crypto;
using System.Security.Cryptography.X509Certificates;

namespace GenSelfSignedCert
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            new GenerateSelfSignedCertForm().ShowDialog();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).ToString());
        }

        // here's a simple example of how to gen a cert programmatically using Pluralsight.Crypto
        // note you'll need to also reference System.Security.dll to get support for X509Certificate2UI.
        static void GenSelfSignedCert()
        {
            using (CryptContext ctx = new CryptContext())
            {
                ctx.Open();

                X509Certificate2 cert = ctx.CreateSelfSignedCertificate(
                    new SelfSignedCertProperties
                    {
                        IsPrivateKeyExportable = true,
                        KeyBitLength = 4096,
                        Name = new X500DistinguishedName("cn=localhost"),
                        ValidFrom = DateTime.Today.AddDays(-1),
                        ValidTo = DateTime.Today.AddYears(1),
                    });

                X509Certificate2UI.DisplayCertificate(cert);
            }
        }
    }
}
