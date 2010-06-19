// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if !SILVERLIGHT // we do not support xml config on SL

namespace Castle.Windsor.Tests.Configuration2
{
	using Castle.Core.Configuration;
	using Castle.Core.Resource;
	using Castle.MicroKernel.SubSystems.Configuration;
	using Castle.Windsor.Configuration.Interpreters;

	using NUnit.Framework;

	[TestFixture]
	public class IncludesTestCase
	{
		private IWindsorContainer container;

		[Test]
		public void FileResourceAndIncludes()
		{
			container = new WindsorContainer(ConfigHelper.ResolveConfigPath("Configuration2/config_with_include.xml"));

			AssertConfiguration();
		}

		[Test]
		public void FileResourceAndRelativeIncludes()
		{
			container = new WindsorContainer(ConfigHelper.ResolveConfigPath("Configuration2/config_with_include_relative.xml"));

			AssertConfiguration();
		}

		[Test]
		public void FileResourceAndRelativeIncludes2()
		{
			container = new WindsorContainer(ConfigHelper.ResolveConfigPath("Configuration2/config_with_include_relative2.xml"));

			AssertConfiguration();
		}

		[Test]
		public void AssemblyResourceAndIncludes()
		{
			IResource resource = new AssemblyResource("assembly://Castle.Windsor.Tests/Configuration2/resource_config_with_include.xml");

			container = new WindsorContainer(new XmlInterpreter(resource));

			AssertConfiguration();
		}

		private void AssertConfiguration()
		{
			IConfigurationStore store = container.Kernel.ConfigurationStore;

			Assert.AreEqual(2, store.GetFacilities().Length);
			Assert.AreEqual(2, store.GetComponents().Length);

			IConfiguration config = store.GetFacilityConfiguration("testidengine");
			IConfiguration childItem = config.Children["item"];
			Assert.IsNotNull(childItem);
			Assert.AreEqual("value", childItem.Value);

			config = store.GetFacilityConfiguration("testidengine2");
			Assert.IsNotNull(config);
			Assert.AreEqual("value within CDATA section", config.Value);

			config = store.GetComponentConfiguration("testidcomponent1");
			childItem = config.Children["item"];
			Assert.IsNotNull(childItem);
			Assert.AreEqual("value1", childItem.Value);

			config = store.GetComponentConfiguration("testidcomponent2");
			childItem = config.Children["item"];
			Assert.IsNotNull(childItem);
			Assert.AreEqual("value2", childItem.Value);
		}
	}
}

#endif