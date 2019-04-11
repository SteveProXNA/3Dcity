using System;
//using System.IO;
//using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace WindowsGame.Master.Factorys
{
	public interface IStorageFactory
	{
		void Initialize(String fileName);
		T LoadContent<T>();
		void SaveContent<T>(T data);
	}

	public class StorageFactory : IStorageFactory
	{
		//private IsolatedStorageFile storage;
		private String myFileName;

		public void Initialize(String fileName)
		{
			myFileName = fileName;
		}

		public T LoadContent<T>()
		{
			T data = default(T);
			//try
			//{
			//	using (storage = GetUserStoreAsAppropriateForCurrentPlatform())
			//	{
			//		if (storage.FileExists(myFileName))
			//		{
			//			// FileStream m_FullPath
			//			using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(myFileName, FileMode.Open, storage))
			//			{
			//				XmlSerializer serializer = new XmlSerializer(typeof(T));
			//				data = (T)serializer.Deserialize(fileStream);
			//			}
			//		}
			//	}
			//}
			//catch
			//{
			//}

			return data;
		}

		public void SaveContent<T>(T data)
		{
			//try
			//{
			//	using (storage = GetUserStoreAsAppropriateForCurrentPlatform())
			//	{
			//		using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(myFileName, FileMode.Create, storage))
			//		{
			//			XmlSerializer serializer = new XmlSerializer(typeof(T));
			//			serializer.Serialize(fileStream, data);
			//		}
			//	}
			//}
			//catch
			//{
			//}
		}

		// http://blogs.msdn.com/b/shawnhar/archive/2010/12/16/isolated-storage-windows-and-clickonce.aspx
//		private static IsolatedStorageFile GetUserStoreAsAppropriateForCurrentPlatform()
//		{
//#if WINDOWS
//			return IsolatedStorageFile.GetUserStoreForDomain();
//#else
//			return IsolatedStorageFile.GetUserStoreForApplication();
//#endif
//		}

	}
}
