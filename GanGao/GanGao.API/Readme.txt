MEF2 for API 实例:
 https://github.com/MicrosoftArchive/mef/blob/master/oob/demo/Microsoft.Composition.Demos.Web.Http/Boundaries.cs
 https://github.com/MicrosoftArchive/mef/blob/master/oob/test/Microsoft.Composition.Demos.Web.Http.UnitTests.Desktop/StandaloneDependencyResolverTests.cs

MEF 调试方法
在 new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection);
添加参数 CompositionOptions.DisableSilentRejection
可以使用预编译
#if DEBUG
new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection);
#else
new CompositionContainer(_catalog);
#endif
