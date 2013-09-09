/* The File Commander - API для плагинов
 * (C) 2013, Alexander Tauenis (atauenis@yandex.ru)
 * Копирование кода разрешается только с письменного согласия
 * разработчика (А.Т.).
 */
using System;
using System.Collections.Generic;

namespace pluginner{
	/// <summary>
	/// Default plugin interface.
	/// </summary>
	public interface IPlugin{
		/// <summary>
		/// Gives the plugin's name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gives the plugin's version.
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Gives the plugin's author.
		/// </summary>
		string Author { get; }
	}

	/// <summary>
	/// Interface for filesystem & archive plugins.
	/// </summary>
	public interface IFSPlugin : IPlugin{
		/// <summary>
		/// Gets or sets the content of the directory.
		/// </summary>
		/// <value>
		/// The content of the directory.
		/// </value>
		List<DirItem> DirectoryContent {get;}

		/// <summary>
		/// Gets or sets the current directory.
		/// </summary>
		/// <value>
		/// The current directory URL.
		/// </value>
		string CurrentDirectory {get; set;}

		/// <summary>
		/// Determines whether at the specified URL exists a file
		/// </summary>
		/// <returns>
		/// <c>true</c> if the file really present; else, retruns <c>false</c>.
		/// </returns>
		/// <param name='URL'>
		/// The file location (URL)
		/// </param>
		bool IsFilePresent(string URL);

		/// <summary>
		/// Determines whether at the specified URL exists a directory
		/// </summary>
		/// <returns>
		/// <c>true</c> if the directory really present; else, retruns <c>false</c>.
		/// </returns>
		/// <param name='URL'>
		/// The directory location (URL)
		/// </param>
		bool IsDirPresent(string URL);

        /// <summary>
        /// Tries to read the file or directory <paramref name="URL"/> and determines whether it can be read without access errors.
        /// </summary>
        /// <param name="URL">The uniform resource locator for the requested file</param>
        /// <returns></returns>
        bool CanBeRead(string URL);

		/// <summary>
		/// Reads the file and returns both file content and it's metadata.
		/// </summary>
		/// <returns>
		/// The file.
		/// </returns>
		/// <param name='URL'>
		/// URL of the file.
		/// </param>
		File GetFile(string URL);

		/// <summary>
		/// Writes the file.
		/// </summary>
		/// <param name='NewFile'>
		/// New file's content.
		/// </param>
		void WriteFile(File NewFile);

		/// <summary>
		/// Removes the file <paramref name="URL"/>.
		/// </summary>
		/// <param name='URL'>
		/// URL of the file.
		/// </param>
		void RemoveFile(string URL);

        /// <summary>
        /// Creates a new directory
        /// </summary>
        /// <param name="URL"></param>
        void MakeDir(string URL);
	}
	//todo: IEditorPlugin, IUIPlugin (плагины к интерфейсу File Commander)

	/// <summary>
	/// File provider.
	/// </summary>
	public struct File{
		/// <summary>
		/// The file's metadata (date, size, etc).
		/// </summary>
		public System.IO.FileInfo Metadata;

		/// <summary>
		/// The file's full path.
		/// </summary>
		public string Path;

		/// <summary>
		/// The file's content.
		/// </summary>
		public byte[] Content;

        /// <summary>
        /// Returns the file's name
        /// </summary>
        public string Name;
	}

	/// <summary>
	/// Directory item info (structure).
	/// </summary>
	public struct DirItem{
		/// <summary>
		/// The path of the file.
		/// </summary>
		public string Path;

		/// <summary>
		/// The text to show in the list.
		/// </summary>
		public string TextToShow;

		/// <summary>
		/// The file's size.
		/// </summary>
		public long Size;

		/// <summary>
		/// The file's date.
		/// </summary>
		public DateTime Date;

		/// <summary>
		/// The file's accessrules.
		/// </summary>
		public string Rules;

		/// <summary>
		/// Is the item a directory? 1=dir, 0=file
		/// </summary>
		public bool IsDirectory;

		/// <summary>
		/// Is the file/directory hidden. 0=maybe showed, 1=dont show
		/// </summary>
		public bool Hidden;
	}

    /// <summary>
    /// Interface for FCView plugins
    /// </summary>
    public interface IViewerPlugin : IPlugin{
        /// <summary>
        /// The control to be displayed in FCView window
        /// </summary>
        System.Windows.Forms.Control DisplayBox();

        /// <summary>
        /// Loads & shows a file into the File Commander Viewer
        /// </summary>
        /// <param name="url"></param>
		void LoadFile(string url, pluginner.IFSPlugin fsplugin);

		/// <summary>
		/// Gets a value indicating whether this plugin can copy content into system clipboard.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance can use clipboard; otherwise, <c>false</c>.
		/// </value>
		bool CanCopy{get;}

		/// <summary>
		/// Copy selected content into the system clipboard.
		/// </summary>
		void Copy();

		/// <summary>
		/// Gets a value indicating whether this plugin can select all content.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance can select all content; otherwise, <c>false</c>.
		/// </value>
		bool CanSelectAll{get;}

		/// <summary>
		/// Selects all content inside this plugin.
		/// </summary>
		void SelectAll();

		/// <summary>
		/// Gets a value indicating whether this plugin can print content.
		/// </summary>
		/// <value>
		/// <c>true</c> if this plugin can print; otherwise, <c>false</c>.
		/// </value>
		bool CanPrint{get;}

		/// <summary>
		/// Print content in this instance.
		/// </summary>
		void Print();

		/// <summary>
		/// Shows print settngs dialog window.
		/// </summary>
		void PrintSettings();

        /// <summary>
        /// Shows plugin settings dialog window
        /// </summary>
        void ShowSettings();
    }

	/// <summary>
	/// Exception, which fires when the plugin module needs to be changed to an other plugin module.
	/// For example, when a filesystem plugin tried to be used with uncompatible filesystem or a image viewer plugin tried to show a text file.
	/// </summary>
	[System.Serializable]
	public class PleaseSwitchPluginException : System.Exception
	{
		/// <summary>
		/// Informs the File Commander that the plugin cannot be used now and must be changed
		/// </summary>
		public PleaseSwitchPluginException ()
		{
		}
		
		/// <summary>
		/// Informs the File Commander that the plugin cannot be used now and must be changed
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception reason. </param>
		public PleaseSwitchPluginException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Informs the File Commander that the plugin cannot be used now and must be changed. The reason should be showed in the <see cref="inner"/>.
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public PleaseSwitchPluginException (string message, System.Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PleaseSwitchPluginException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected PleaseSwitchPluginException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
}

