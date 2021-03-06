﻿using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Unity.Tests.Fixtures;
using NSubstitute;
using Xunit;

namespace Cake.Unity.Tests.Unit
{
    public sealed class UnityRunnerTests
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given   
            var fixture = new UnityRunnerFixture();
            fixture.Context = null;                

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("context", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void Should_Throw_If_Project_Path_Is_Null()
        {
            // Given
            var fixture = new UnityRunnerFixture();
            fixture.ProjectPath = null;

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("projectPath", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void Should_Throw_If_Action_Is_Null()
        {
            // Given
            var fixture = new UnityRunnerFixture();
            fixture.Action = null;

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("action", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void Should_Throw_If_Project_Path_Do_Not_Exist()
        {
            // Given
            var fixture = new UnityRunnerFixture();
            fixture.ProjectPathExist = false;

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("The Unity project C:/Project do not exist.", result.Message);
        }

        [Fact]
        public void Should_Throw_If_Unity_Is_Not_Installed()
        {
            // Given
            var fixture = new UnityRunnerFixture();
            fixture.DefaultToolPathExist = false;

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("Unity: Could not locate executable.", result.Message);
        }

        [Fact]
        public void Should_Set_Default_Arguments()
        {
            // Given
            var fixture = new UnityRunnerFixture();

            // When
            fixture.ExecuteRunner();

            // Then
            fixture.ProcessRunner.Received(1).Start(
                Arg.Any<FilePath>(),
                Arg.Is<ProcessSettings>(p =>
                    p.Arguments.Render() == "-batchmode -quit -projectPath \"C:/Project\""));
        }

        [Fact]
        public void Should_Throw_If_Process_Was_Not_Started()
        {
            // Given
            var fixture = new UnityRunnerFixture();
            fixture.ProcessRunner.Start(Arg.Any<FilePath>(), Arg.Any<ProcessSettings>()).Returns((IProcess)null);

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("Unity: Process was not started.", result.Message);
        }

        [Fact]
        public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
        {
            // Given
            var fixture = new UnityRunnerFixture();
            fixture.Process.SetExitCode(1);

            // When
            var result = Record.Exception(() => fixture.ExecuteRunner());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("Unity: Process returned an error (exit code 1).", result.Message);
        }
    }
}
