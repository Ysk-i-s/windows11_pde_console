using System;
using System.Threading.Tasks;
using Windows.Security.DataProtection;
using Windows.Storage;

namespace windows11_pde_console
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("[-] arguments error.");
                Console.WriteLine("Example) pde.exe target.txt 2");
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                return;
            }

            string targetPath = args[0];

            if (!System.IO.File.Exists(targetPath))
            {
                Console.WriteLine("[-] Target file is not exist.");
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                return;
            }

            int encryptionLevel = 0;
            if(!int.TryParse(args[1], out encryptionLevel))
            {
                Console.WriteLine("[-] second argument type error. must be integer.");
                Console.WriteLine("Example) pde.exe target.txt 2");
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                return;
            }

            if(encryptionLevel < 0 || encryptionLevel > 2)
            {
                Console.WriteLine("[-] encryption level error. must be 0 ~ 2.");
                Console.WriteLine("Example) pde.exe target.txt 2");
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                return;
            }

            Task task = Task.Run(() =>
            {
                Encryption(targetPath, encryptionLevel);
            });

            task.Wait();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        static async void Encryption(string targetPath, int encryptionLevel)
        {
            UserDataProtectionManager userDataProtectionManager = UserDataProtectionManager.TryGetDefault();

            // null check must!!!
            if (userDataProtectionManager == null)
            {
                Console.WriteLine("[-] UserDataProtectionManager is not supported on this PC.");
                Console.WriteLine("Requirements: Windows11 22H2 Pro or higher, Azure AD join, Intune PDE policy enable.");
                return;
            }

            StorageFile file;
            try
            {
                file = await StorageFile.GetFileFromPathAsync(targetPath);
            }
            catch
            {
                Console.WriteLine("[-] Target file error. please type absolute path");
                return;
            }

            UserDataStorageItemProtectionStatus status = UserDataStorageItemProtectionStatus.Succeeded;

            file = await StorageFile.GetFileFromPathAsync(targetPath);

            // Encrypt file
            status = await userDataProtectionManager.ProtectStorageItemAsync(
                file, (UserDataAvailability)Enum.ToObject(typeof(UserDataAvailability), encryptionLevel)
                );

            if (status == UserDataStorageItemProtectionStatus.Succeeded)
            {
                Console.WriteLine("[+] personal data encryption successfully completed");
            }
            else
            {
                Console.WriteLine($"[-] personal data encryption failed. {status}");
            }
        }
    }
}
