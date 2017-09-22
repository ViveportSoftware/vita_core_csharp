﻿using System.IO;
using Htc.Vita.Core.Diagnosis;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void FileSignatureInfo_Default_0_GetSignatureInfo()
        {
            var fileInfo = new FileInfo("C:\\Windows\\System32\\svchost.exe");
            Assert.True(fileInfo.Exists);
            var fileSignatureInfo = FileSignatureInfo.GetSignatureInfo(fileInfo);
            Assert.NotNull(fileSignatureInfo);
            Assert.True(!string.IsNullOrEmpty(fileSignatureInfo.IssuerDistinguishedName));
            Assert.True(fileSignatureInfo.IssuerDistinguishedName.Contains("O="));
            Assert.True(!string.IsNullOrEmpty(fileSignatureInfo.IssuerName));
            Assert.True(!string.IsNullOrEmpty(fileSignatureInfo.SubjectDistinguishedName));
            Assert.True(fileSignatureInfo.SubjectDistinguishedName.Contains("O="));
            Assert.True(!string.IsNullOrEmpty(fileSignatureInfo.SubjectName));
            Assert.True(!string.IsNullOrEmpty(fileSignatureInfo.PublicKey));
            Assert.True(fileSignatureInfo.Verified);
        }

        [Fact]
        public void FileSignatureInfo_Default_0_GetSignatureInfo_WithNotepad()
        {
            var fileInfo = new FileInfo("C:\\Windows\\System32\\notepad.exe");
            Assert.True(fileInfo.Exists);
            var fileSignatureInfo = FileSignatureInfo.GetSignatureInfo(fileInfo);
            Assert.NotNull(fileSignatureInfo);
            Assert.True(string.IsNullOrEmpty(fileSignatureInfo.IssuerDistinguishedName));
            Assert.True(string.IsNullOrEmpty(fileSignatureInfo.IssuerName));
            Assert.True(string.IsNullOrEmpty(fileSignatureInfo.SubjectDistinguishedName));
            Assert.True(string.IsNullOrEmpty(fileSignatureInfo.SubjectName));
            Assert.True(string.IsNullOrEmpty(fileSignatureInfo.PublicKey));
            Assert.False(fileSignatureInfo.Verified);
        }
    }
}
