using System;
using System.IO;

namespace WindowsGame.Master.Interfaces
{
	public interface IFileProxy
	{
		Stream GetStream(String path);
	}
}
