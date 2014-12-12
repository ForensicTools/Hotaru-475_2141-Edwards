#pragma once
#include <tsk/libtsk.h>
#include <cstring>
#include <string>
#include <vcclr.h>

namespace Hotaru {

	using namespace System;
	using namespace System::IO;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MainForm
	/// </summary>
	public ref class MainForm : public System::Windows::Forms::Form
	{
		static System::Collections::Specialized::StringCollection ^ log = gcnew System::Collections::Specialized::StringCollection();
		public:
			MainForm(void)
			{
				InitializeComponent();
				//
				//TODO: Add the constructor code here
				//
			}

		protected:
			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			~MainForm()
			{
				if (components)
				{
					delete components;
				}
			}

		private: System::Windows::Forms::MenuStrip^  menuStrip;
		protected:

		private: System::Windows::Forms::ToolStripMenuItem^  fileMenu;
		private: System::Windows::Forms::ToolStripMenuItem^  addImageMenu;
		protected:


		private: System::Windows::Forms::ToolStripMenuItem^  exitMenu;
		private: System::Windows::Forms::OpenFileDialog^  openFileDialog;
		private: System::Windows::Forms::SplitContainer^  splitContainer;
		private: System::Windows::Forms::TreeView^  treeView;

		private: System::Windows::Forms::ImageList^  imageList;
		private: System::Windows::Forms::SplitContainer^  splitContainer1;
		private: System::ComponentModel::IContainer^  components;



		private:
			/// <summary>
			/// Required designer variable.
			/// </summary>


		#pragma region Windows Form Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			void InitializeComponent(void)
			{
				this->components = (gcnew System::ComponentModel::Container());
				System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(MainForm::typeid));
				this->menuStrip = (gcnew System::Windows::Forms::MenuStrip());
				this->fileMenu = (gcnew System::Windows::Forms::ToolStripMenuItem());
				this->addImageMenu = (gcnew System::Windows::Forms::ToolStripMenuItem());
				this->exitMenu = (gcnew System::Windows::Forms::ToolStripMenuItem());
				this->openFileDialog = (gcnew System::Windows::Forms::OpenFileDialog());
				this->splitContainer = (gcnew System::Windows::Forms::SplitContainer());
				this->splitContainer1 = (gcnew System::Windows::Forms::SplitContainer());
				this->treeView = (gcnew System::Windows::Forms::TreeView());
				this->imageList = (gcnew System::Windows::Forms::ImageList(this->components));
				this->menuStrip->SuspendLayout();
				(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->splitContainer))->BeginInit();
				this->splitContainer->Panel1->SuspendLayout();
				this->splitContainer->SuspendLayout();
				(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->splitContainer1))->BeginInit();
				this->splitContainer1->Panel1->SuspendLayout();
				this->splitContainer1->SuspendLayout();
				this->SuspendLayout();
				// 
				// menuStrip
				// 
				this->menuStrip->BackgroundImageLayout = System::Windows::Forms::ImageLayout::None;
				this->menuStrip->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->fileMenu });
				this->menuStrip->Location = System::Drawing::Point(0, 0);
				this->menuStrip->Name = L"menuStrip";
				this->menuStrip->RenderMode = System::Windows::Forms::ToolStripRenderMode::System;
				this->menuStrip->Size = System::Drawing::Size(903, 24);
				this->menuStrip->TabIndex = 0;
				this->menuStrip->Text = L"menuStrip1";
				// 
				// fileMenu
				// 
				this->fileMenu->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {
					this->addImageMenu,
						this->exitMenu
				});
				this->fileMenu->Name = L"fileMenu";
				this->fileMenu->Size = System::Drawing::Size(37, 20);
				this->fileMenu->Text = L"File";
				// 
				// addImageMenu
				// 
				this->addImageMenu->Name = L"addImageMenu";
				this->addImageMenu->Size = System::Drawing::Size(141, 22);
				this->addImageMenu->Text = L"Add Image...";
				this->addImageMenu->Click += gcnew System::EventHandler(this, &MainForm::addImageMenu_Click);
				// 
				// exitMenu
				// 
				this->exitMenu->Name = L"exitMenu";
				this->exitMenu->Size = System::Drawing::Size(141, 22);
				this->exitMenu->Text = L"Exit";
				this->exitMenu->Click += gcnew System::EventHandler(this, &MainForm::exitMenu_Click);
				// 
				// openFileDialog
				// 
				this->openFileDialog->FileName = L"openFileDialog";
				// 
				// splitContainer
				// 
				this->splitContainer->Dock = System::Windows::Forms::DockStyle::Fill;
				this->splitContainer->Location = System::Drawing::Point(0, 24);
				this->splitContainer->Name = L"splitContainer";
				this->splitContainer->Orientation = System::Windows::Forms::Orientation::Horizontal;
				// 
				// splitContainer.Panel1
				// 
				this->splitContainer->Panel1->Controls->Add(this->splitContainer1);
				this->splitContainer->Size = System::Drawing::Size(903, 529);
				this->splitContainer->SplitterDistance = 367;
				this->splitContainer->TabIndex = 1;
				// 
				// splitContainer1
				// 
				this->splitContainer1->Dock = System::Windows::Forms::DockStyle::Fill;
				this->splitContainer1->Location = System::Drawing::Point(0, 0);
				this->splitContainer1->Name = L"splitContainer1";
				// 
				// splitContainer1.Panel1
				// 
				this->splitContainer1->Panel1->Controls->Add(this->treeView);
				this->splitContainer1->Size = System::Drawing::Size(903, 367);
				this->splitContainer1->SplitterDistance = 301;
				this->splitContainer1->TabIndex = 1;
				// 
				// treeView
				// 
				this->treeView->Dock = System::Windows::Forms::DockStyle::Fill;
				this->treeView->ImageIndex = 0;
				this->treeView->ImageList = this->imageList;
				this->treeView->Location = System::Drawing::Point(0, 0);
				this->treeView->Name = L"treeView";
				this->treeView->SelectedImageIndex = 0;
				this->treeView->Size = System::Drawing::Size(301, 367);
				this->treeView->TabIndex = 0;
				// 
				// imageList
				// 
				this->imageList->ImageStream = (cli::safe_cast<System::Windows::Forms::ImageListStreamer^>(resources->GetObject(L"imageList.ImageStream")));
				this->imageList->TransparentColor = System::Drawing::Color::Transparent;
				this->imageList->Images->SetKeyName(0, L"Folder-icon.png");
				// 
				// MainForm
				// 
				this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
				this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
				this->AutoScroll = true;
				this->BackColor = System::Drawing::SystemColors::Control;
				this->ClientSize = System::Drawing::Size(903, 553);
				this->Controls->Add(this->splitContainer);
				this->Controls->Add(this->menuStrip);
				this->MainMenuStrip = this->menuStrip;
				this->Name = L"MainForm";
				this->Text = L"Hotaru";
				this->Load += gcnew System::EventHandler(this, &MainForm::MainForm_Load);
				this->menuStrip->ResumeLayout(false);
				this->menuStrip->PerformLayout();
				this->splitContainer->Panel1->ResumeLayout(false);
				(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->splitContainer))->EndInit();
				this->splitContainer->ResumeLayout(false);
				this->splitContainer1->Panel1->ResumeLayout(false);
				(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->splitContainer1))->EndInit();
				this->splitContainer1->ResumeLayout(false);
				this->ResumeLayout(false);
				this->PerformLayout();

			}

		#pragma endregion
		private: System::Void addImageMenu_Click(System::Object^  sender, System::EventArgs^  e) {
				OpenFileDialog ^ openImage = gcnew OpenFileDialog();
				openImage->Filter = "Image Files|*.dd, *.img, *.raw, *.aff|All Files|*.*";
				if (openImage->ShowDialog() == System::Windows::Forms::DialogResult::OK){
					//TskImgInfo * image = tsk_img_open_sing(
				}
		}

		private: System::Void exitMenu_Click(System::Object^  sender, System::EventArgs^  e) {
				Application::Exit();
		}
		
		private: System::Void PopulateFolders(String ^ dir, TreeNode ^ node){
				DirectoryInfo ^ directory = gcnew DirectoryInfo(dir);
				try{
					array<DirectoryInfo^>^ directories = directory->GetDirectories();
					for each (DirectoryInfo ^ d in directories)
					{
						TreeNode ^ t = gcnew TreeNode(d->Name);
						PopulateFolders(d->FullName, t);
						node->Nodes->Add(d->Name);
					}
				}
				catch (System::Exception ^ e){
					return;
				}
		}

		private: System::Void MainForm_Load(System::Object^  sender, System::EventArgs^  e) {
				// Lists the drives and folders on the system
				for each (DriveInfo^ drive in DriveInfo::GetDrives())
				{
					TreeNode ^ newNode = gcnew TreeNode(drive->Name);
					treeView->Nodes->Add(newNode);
					/*DirectoryInfo ^ directoryInfo = gcnew DirectoryInfo(drive->Name);
					array<DirectoryInfo^>^ directories = directoryInfo->GetDirectories();
					if (directories->Length > 0){
						for each (DirectoryInfo^ d in directories)
						{
							newNode->Nodes->Add(d->Name);
						}
					}*/
					PopulateFolders(drive->RootDirectory->FullName, newNode);
				}
		}
	};
}
