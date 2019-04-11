using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Master.Managers
{
	public class FileManager : IFileManager
	{
		private readonly IFileProxy fileProxy;

		public FileManager(IFileProxy fileProxy)
		{
			this.fileProxy = fileProxy;
		}

		public IList<String> LoadTxt(String file)
		{
			return LoadTxt<String>(file);
		}

		public IList<T> LoadTxt<T>(String file)
		{
			IList<T> lines = new List<T>();
			using (Stream stream = fileProxy.GetStream(file))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					String line = reader.ReadLine();
					while (line != null)
					{
						T data = GetValue<T>(line);
						lines.Add(data);
						line = reader.ReadLine();
					}
				}
			}

			return lines;
		}

		public T LoadXml<T>(String file)
		{
			T data;
			using (Stream stream = fileProxy.GetStream(file))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				data = (T)serializer.Deserialize(stream);
			}

			return data;
		}

		public XElement LoadXElement(String file)
		{
			XElement root;
			using (Stream stream = fileProxy.GetStream(file))
			{
				XDocument document = XDocument.Load(stream);
				root = document.Root;
			}

			return root;
		}

		// https://stackoverflow.com/questions/732677/converting-from-string-to-t
		private static T GetValue<T>(String value)
		{
			return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
		}

	}
}