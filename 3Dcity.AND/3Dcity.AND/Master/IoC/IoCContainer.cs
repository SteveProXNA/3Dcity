using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;

namespace WindowsGame.Master.IoC
{
	/// <summary>
	/// IoCContainer will resolve/release all applicable object references.
	/// </summary>
	public static class IoCContainer
	{
		private static IKernel kernel;

		private static IDictionary<Type, Type> data = null;

		public static void Initialize<T, TImplementation>() where TImplementation : T
		{
			if (null == data)
			{
				data = new Dictionary<Type, Type>();
			}

			Type t1 = typeof(T);
			Type t2 = typeof(TImplementation);

			if (!data.ContainsKey(t1))
			{
				data.Add(t1, t2);
			}
			else
			{
				data[t1] = t2;
			}
		}

		public static T Resolve<T>()
		{
			if (null == kernel)
			{
				INinjectModule module = new EngineModule(data);
				INinjectModule[] modules = new[] { module };

				kernel = new StandardKernel(modules);
			}

			return kernel.Get<T>();
		}

		public static void Release()
		{
			if (null == kernel)
			{
				return;
			}

			kernel.Dispose();
			kernel = null;
		}
	}
}