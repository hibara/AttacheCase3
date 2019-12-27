﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exeout.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Exeout.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Access to the file has been denied.
        ///Move to a place (eg Desktop) where you can read and write files and try again. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageAccessDeny {
            get {
                return ResourceManager.GetString("DialogMessageAccessDeny", resourceCulture);
            }
        }
        
        /// <summary>
        ///   This encrypted file is broken. The process is aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageAtcFileBroken {
            get {
                return ResourceManager.GetString("DialogMessageAtcFileBroken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Encrypted data not found. The file is broken.
        ///Decryption failed. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageDataNotFound {
            get {
                return ResourceManager.GetString("DialogMessageDataNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Password is invalid, or the encrypted file might have been broken. Decryption is aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageDecryptionError {
            get {
                return ResourceManager.GetString("DialogMessageDecryptionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Internal file index is invalid in encrypted file. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageFileIndexInvalid {
            get {
                return ResourceManager.GetString("DialogMessageFileIndexInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The path of files or folders are invalid. The process is aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageInvalidFilePath {
            get {
                return ResourceManager.GetString("DialogMessageInvalidFilePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   No free space on the disk. The process is aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageNoDiskSpace {
            get {
                return ResourceManager.GetString("DialogMessageNoDiskSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The file is not encrypted file. The process is aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageNotAtcFile {
            get {
                return ResourceManager.GetString("DialogMessageNotAtcFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The file is not the same hash value. Whether the file is corrupted, it may have been made the falsification.The process is aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageNotSameHash {
            get {
                return ResourceManager.GetString("DialogMessageNotSameHash", resourceCulture);
            }
        }
        
        /// <summary>
        ///   An unexpected error has occurred. And stops processing. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogMessageUnexpectedError {
            get {
                return ResourceManager.GetString("DialogMessageUnexpectedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Alert に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogTitleAlert {
            get {
                return ResourceManager.GetString("DialogTitleAlert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Error に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogTitleError {
            get {
                return ResourceManager.GetString("DialogTitleError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Infomation に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogTitleInfomation {
            get {
                return ResourceManager.GetString("DialogTitleInfomation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Question に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DialogTitleQuestion {
            get {
                return ResourceManager.GetString("DialogTitleQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Aborted. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelCaptionAborted {
            get {
                return ResourceManager.GetString("labelCaptionAborted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Canceled. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelCaptionCanceled {
            get {
                return ResourceManager.GetString("labelCaptionCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Completed. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelCaptionCompleted {
            get {
                return ResourceManager.GetString("labelCaptionCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Error occurred. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelCaptionError {
            get {
                return ResourceManager.GetString("labelCaptionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The following directory already exists. Do you overwrite the directory to save? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelComfirmToOverwriteDir {
            get {
                return ResourceManager.GetString("labelComfirmToOverwriteDir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The following file already exists. Do you overwrite the files to save? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelComfirmToOverwriteFile {
            get {
                return ResourceManager.GetString("labelComfirmToOverwriteFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Getting ready for decryption... に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelGettingReadyForDecryption {
            get {
                return ResourceManager.GetString("labelGettingReadyForDecryption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Input Password: に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string labelInputPassword {
            get {
                return ResourceManager.GetString("labelInputPassword", resourceCulture);
            }
        }
    }
}
