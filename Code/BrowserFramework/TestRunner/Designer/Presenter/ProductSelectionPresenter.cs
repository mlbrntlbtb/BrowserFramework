using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Designer.View;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System.Security.Permissions;

namespace TestRunner.Designer.Presenter
{
    public class ProductSelectionPresenter
    {
        #region PRIVATE MEMBERS
        private IProductSelectionView mView = null;
        private static readonly string INIT_FILE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Init.dat");
        #endregion

        #region PUBLIC MEMBERS
        public string mRootPath = string.Empty;
        public string DirectoryFilter = string.Empty;

        public ProductSelectionPresenter(IProductSelectionView view)
        {
            this.mView = view;
            mRootPath = GetRootPath();
        }
        #endregion
        
        #region PRIVATE METHODS
        
        /// <summary>
        /// Checks if the required folders are existing
        /// </summary>
        private bool CompleteProductFiles(string rootPath, string productFolder)
        {
            string dirProductRoot = Path.Combine(rootPath, "Products") + @"\";
            if (!Directory.Exists(dirProductRoot))
            {
                return false;
            }
            string dirProduct = Path.Combine(dirProductRoot, productFolder) + @"\";
            if (!Directory.Exists(dirProduct))
            {
                return false;
            }
            string dirFramework = Path.Combine(dirProduct, "Framework") + @"\";
            if (!Directory.Exists(dirFramework))
            {
                return false;
            }
            string dirObjectStore = Path.Combine(dirFramework, "ObjectStore") + @"\";
            if (!Directory.Exists(dirObjectStore))
            {
                return false;
            }
            string dirTestSuite = Path.Combine(dirProduct, "Suites") + @"\";
            if (!Directory.Exists(dirTestSuite))
            {
                return false;
            }

            string dirTests = Path.Combine(dirProduct, "Tests") + @"\";
            if (!Directory.Exists(dirTests))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get root path based from executing assembly path
        /// </summary>
        /// <returns></returns>
        private string GetRootPath()
        {
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            mRootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(mRootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }
            return mRootPath;
        }
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Loads all available products
        /// </summary>
        public void LoadAvailable()
        {

            mView.AvailableProducts.Clear();
            XDocument init = XDocument.Load(INIT_FILE);
            var products = from product in init.Descendants("application")
                           select new
                           {
                               id = product.Attribute("id").Value,
                               type = product.Attribute("type").Value,
                               name = product.Attribute("name").Value,
                               version = product.Attribute("version").Value,
                               library = product.Attribute("library").Value,
                               productfolder = product.Attribute("productfolder").Value,
                               platform = product.Attribute("platform").Value
                           };
            List<DlkTargetApplication> ap = new List<DlkTargetApplication>();
            foreach (var product in products)
            {
                if (CompleteProductFiles(mRootPath, product.productfolder))
                {
                    ap.Add(new DlkTargetApplication
                    {
                        ID = product.id,
                        Name = product.name,
                        Version = product.version,
                        Library = product.library,
                        ProductFolder = product.productfolder,
                        Type = product.type,
                        Platform = product.platform
                    }
                        );
                }
            }
            ap = ap.OrderBy(x => x.Name).ToList();
            mView.AvailableProducts = ap;
        }

        /// <summary>
        /// Change critical environment variables in memory and file, based from selected product
        /// </summary>
        public void CommitSelectedProduct()
        {
            DlkEnvironment.InitializeEnvironment(mView.SelectedProduct.ProductFolder, mRootPath, mView.SelectedProduct.Library, mView.SelectedProduct.DisplayName);
            DlkTestRunnerSettingsHandler.Initialize(mRootPath);
        }

        /// <summary>
        /// Save directory to xml file as a reference when TL is loaded again
        /// </summary>
        /// <param name="DirectoryFilter">Selected suite directory filter</param>
        /// <param name="XmlFilePath">file path where the selected suite directory will be stored</param>
        public void SaveDirectory(string DirectoryFilter, string XmlFilePath)
        {
            /* Ensure directory file exists */
            if (!File.Exists(XmlFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(XmlFilePath));
                File.WriteAllText(XmlFilePath, "<directory />");
            }

            XElement mRoot = new XElement("directory",
                new XElement("path", DirectoryFilter));

            File.SetAttributes(XmlFilePath, FileAttributes.Normal);
            FileIOPermission fPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, XmlFilePath);

            mRoot.Save(XmlFilePath);
        }
        #endregion

    }
}
