using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForTraining
{
    public static class FileController
    {

        //public static List<Contact> GetAllContacts()
        //{
        //    string fileName = "Contacts.txt";
        //    var mainpath = Application.StartupPath.Replace("\\bin\\Debug", "");
        //    string path = Path.Combine(mainpath, fileName);
        //    List<Contact> contacts = new List<Contact>();
        //    if (File.Exists(path))
        //    {

        //        using (StreamReader sr = File.OpenText(path))
        //        {
        //            String s = "";

        //            while ((s = sr.ReadLine()) != null)
        //            {
        //                Contact contact = new Contact();
        //                contact.ID = int.Parse(s.Split(',')[0]);
        //                contact.Name = s.Split(',')[1];
        //                contact.Phone = s.Split(',')[2];
        //                contact.Image = s.Split(',')[3];
        //                contacts.Add(contact);
        //            }
        //        }
        //    }
        //    return contacts;
        //}
        public static List<Contact> GetAllContacts(string key)
        {
            string fileName = "Contacts.txt";
            var mainpath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string path = Path.Combine(mainpath, fileName);
            List<Contact> contacts = new List<Contact>();
            if (File.Exists(path))
            {

                using (StreamReader sr = File.OpenText(path))
                {
                    String s = "";

                    while ((s = sr.ReadLine()) != null)
                    {
                        Contact contact = new Contact();
                        contact.ID = int.Parse(s.Split(',')[0]);
                        contact.Name = s.Split(',')[1];
                        contact.Phone = s.Split(',')[2];
                        contact.Image = s.Split(',')[3];
                        if (string.IsNullOrEmpty(key) || contact.ID.ToString() == key
                            || contact.Name.Contains(key)
                            || contact.Phone == key)
                            contacts.Add(contact);
                    }
                }
            }
            return contacts;
        }
        public static void UpdateContact(Contact contact, bool NewImage)
        {
            string fileName = "Contacts.txt";
            var mainpath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string path = Path.Combine(mainpath, fileName);
            string[] lines = File.ReadAllLines(path);
            string toDelete = "";
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                if (int.Parse(parts[0]) == contact.ID)
                {
                    toDelete = parts.Last();
                    lines[i] = contact.ID + "," + contact.Name + "," + contact.Phone + "," + (NewImage ? Path.GetFileName(contact.Image) : parts.Last());

                }
            }
            File.WriteAllLines(path, lines);
            string appPath = Path.GetDirectoryName(Application.ExecutablePath).Replace("\\bin\\Debug", "") + @"\media\";
            if (NewImage)
            {
                File.Delete(Path.Combine(appPath, string.Format("{0}" + toDelete, contact.ID.ToString())));
                File.Copy(contact.Image, appPath + contact.ID + Path.GetFileName(contact.Image));
            }
        }

        public static Contact GetContactByID(int id)
        {
            string fileName = "Contacts.txt";
            var mainpath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string path = Path.Combine(mainpath, fileName);

            var c = File.ReadAllLines(path).Where(line => int.Parse(line.Split(',')[0].Trim()) == id).FirstOrDefault();
            if (c != null)
            {
                Contact contact = new Contact();
                contact.Name = c.Split(',')[1];
                contact.Phone = c.Split(',')[2];
                contact.Image = c.Split(',')[3];
                return contact;
            }
            return null;
        }

        public static void AddContact(Contact contact)
        {
            string fileName = "Contacts.txt";
            var mainpath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string path = Path.Combine(mainpath, fileName);
            using (StreamWriter sr = File.AppendText(path))
            {
                sr.WriteLine(contact.ID + "," + contact.Name + "," + contact.Phone + "," + Path.GetFileName(contact.Image));
                sr.Close();
            }
            //save image in folder
            string appPath = Path.GetDirectoryName(Application.ExecutablePath).Replace("\\bin\\Debug", "") + @"\media\";
            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }
            File.Copy(contact.Image, appPath + contact.ID + Path.GetFileName(contact.Image));
        }

        public static void DeleteContact(int id, string image)
        {
            string fileName = "Contacts.txt";
            var mainpath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string path = Path.Combine(mainpath, fileName);

            var lines = File.ReadAllLines(path).Where(line => int.Parse(line.Split(',')[0].Trim()) != id).ToArray();
            File.WriteAllLines(path, lines);

            string appPath = Path.GetDirectoryName(Application.ExecutablePath).Replace("\\bin\\Debug", "") + @"\media\";

            File.Delete(Path.Combine(appPath, id.ToString() + image));
        }
    }
}
