// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Readability", Scope = "member", Target = "~M:ArchitectureTests.Layers.LayerTests.ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Readability", Scope = "member", Target = "~M:ArchitectureTests.Layers.LayerTests.ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Readability", Scope = "member", Target = "~M:ArchitectureTests.Layers.LayerTests.Domain_Should_NotHaveDependencyOnApplication")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Readability", Scope = "member", Target = "~M:ArchitectureTests.Layers.LayerTests.DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Readability", Scope = "member", Target = "~M:ArchitectureTests.Layers.LayerTests.DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Readability", Scope = "member", Target = "~M:ArchitectureTests.Layers.LayerTests.InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer")]
