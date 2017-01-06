using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO.Compression;
using boxalino_client_SDK_CSharp.Exception;
using System.Web;
using System.Collections.Immutable;

namespace boxalino_client_SDK_CSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class BxData
    {
        const string URL_VERIFY_CREDENTIALS = "/frontend/dbmind/en/dbmind/api/credentials/verify";
        const string URL_XML = "/frontend/dbmind/en/dbmind/api/data/source/update";
        const string URL_PUBLISH_CONFIGURATION_CHANGES = "/frontend/dbmind/en/dbmind/api/configuration/publish/owner";
        const string URL_ZIP = "/frontend/dbmind/en/dbmind/api/data/push";

        const string URL_EXECUTE_TASK = "/frontend/dbmind/en/dbmind/files/task/execute";

        private BxClient bxClient;
        private string[] languages;
        private bool isDev;
        private bool isDelta;
        Dictionary<string, string> sourceIdContainers = new Dictionary<string, string>();
        private NestedDictionary<string, object> sources = new NestedDictionary<string, object>();
        Dictionary<string, object> ftpSources = new Dictionary<string, object>();
        private string host = "http://di1.bx-cloud.com";


        private string owner = "bx_client_data_api";

        /// <summary>
        /// Initializes a new instance of the <see cref="BxData"/> class.
        /// </summary>
        /// <param name="bxClient">The bx client.</param>
        /// <param name="languages">The languages.</param>
        /// <param name="isDev">if set to <c>true</c> [is dev].</param>
        /// <param name="isDelta">if set to <c>true</c> [is delta].</param>
        public BxData(BxClient bxClient, string[] languages, bool isDev = false, bool isDelta = false)
        {
            this.bxClient = bxClient;
            this.languages = languages;
            this.isDev = isDev;
            this.isDelta = isDelta;
        }
        /// <summary>
        /// Adds the source string field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="col">The col.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceStringField(string sourceKey, string fieldName, object col, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, fieldName, "string", false, col, referenceSourceKey, validate);
        }

        /// <summary>
        /// Adds the source number field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="col">The col.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceNumberField(string sourceKey, string fieldName, string col, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, fieldName, "number", false, col, referenceSourceKey, validate);
        }
        /// <summary>
        /// Adds the main CSV item file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="itemIdColumn">The item identifier column.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string addMainCSVItemFile(string filePath, string itemIdColumn, string encoding = "UTF-8", string delimiter = ",", string enclosure = "\"", string escape = "\\\\", string lineSeparator = "\\n", string sourceId = "item_vals", string container = "products", bool validate = true)
        {
            string sourceKey = this.addCSVItemFile(filePath, itemIdColumn, encoding, delimiter, enclosure, escape, lineSeparator, sourceId, container, validate);
            this.addSourceIdField(sourceKey, itemIdColumn, null, validate);
            this.addSourceStringField(sourceKey, "bx_item_id", itemIdColumn, null, validate);
            return sourceKey;
        }
        /// <summary>
        /// Sets the field is multi valued.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="multiValued">if set to <c>true</c> [multi valued].</param>
        public void setFieldIsMultiValued(string sourceKey, string fieldName, bool multiValued = true)
        {
            this.addFieldParameter(sourceKey, fieldName, "multiValued", multiValued ? "true" : "false");
        }
        /// <summary>
        /// Adds the source parameter.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <exception cref="BoxalinoException">trying to add a source parameter on sourceId " + sourceId + ", container " + container + " while this source doesn't exist</exception>
        public void addSourceParameter(string sourceKey, string parameterName, string parameterValue)
        {
            string container = this.decodeSourceKey(sourceKey)[0].Trim();
            string sourceId = this.decodeSourceKey(sourceKey)[1].Trim();
            if (((NestedDictionary<string, object>)sources[container])[sourceId] == null)
            {
                throw new BoxalinoException("trying to add a source parameter on sourceId " + sourceId + ", container " + container + " while this source doesn't exist");
            }
            ((NestedDictionary<string, object>)((NestedDictionary<string, object>)this.sources[container])[sourceId])[parameterName].Value = parameterValue;
        }

        /// <summary>
        /// Adds the source customer guest property.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void addSourceCustomerGuestProperty(string sourceKey, string parameterValue)
        {
            this.addSourceParameter(sourceKey, "guest_property_id", parameterValue);
        }
        /// <summary>
        /// Adds the field parameter.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <exception cref="BoxalinoException">trying to add a field parameter on sourceId " + sourceId + ", container " + container + ", fieldName " + fieldName + " while this field doesn't exist</exception>
        public void addFieldParameter(string sourceKey, string fieldName, string parameterName, string parameterValue)
        {
            string container = this.decodeSourceKey(sourceKey)[0].Trim();
            string sourceId = this.decodeSourceKey(sourceKey)[1].Trim();

            if (string.IsNullOrEmpty(this.sources[container][sourceId]["fields"].ToString()))
            {
                throw new BoxalinoException("trying to add a field parameter on sourceId " + sourceId + ", container " + container + ", fieldName " + fieldName + " while this field doesn't exist");
            }

            if (string.IsNullOrEmpty(this.sources[container][sourceId]["fields"][fieldName]["fieldParameters"].ToString()))
            {
                this.sources[container][sourceId]["fields"][fieldName]["fieldParameters"] = new NestedDictionary<string, object>();
            }
            this.sources[container][sourceId]["fields"][fieldName]["fieldParameters"][parameterName].Value = parameterValue;

       
        }

        /// <summary>
        /// Sets the FTP source.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="remoteDir">The remote dir.</param>
        /// <param name="protocol">The protocol.</param>
        /// <param name="type">The type.</param>
        /// <param name="logontype">The logontype.</param>
        /// <param name="timezoneoffset">The timezoneoffset.</param>
        /// <param name="pasvMode">The pasv mode.</param>
        /// <param name="maximumMultipeConnections">The maximum multipe connections.</param>
        /// <param name="encodingType">Type of the encoding.</param>
        /// <param name="bypassProxy">The bypass proxy.</param>
        /// <param name="syncBrowsing">The synchronize browsing.</param>
        public void setFtpSource(string sourceKey, string host = "di1.bx-cloud.com", int port = 21, string user = null, string password = null, string remoteDir = "/sources/production", int protocol = 0, int type = 0, int logontype = 1,
                int timezoneoffset = 0, string pasvMode = "MODE_DEFAULT", int maximumMultipeConnections = 0, string encodingType = "Auto", int bypassProxy = 0, int syncBrowsing = 0)
        {

            if (user == null)
            {
                user = this.bxClient.getAccount(false);
            }

            if (password == null)
            {
                password = this.bxClient.getPassword();
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["Host"] = host;
            parameters["Port"] = port;
            parameters["User"] = user;
            parameters["Pass"] = password;
            parameters["Protocol"] = protocol;
            parameters["Type"] = type;
            parameters["Logontype"] = logontype;
            parameters["TimezoneOffset"] = timezoneoffset;
            parameters["PasvMode"] = pasvMode;
            parameters["MaximumMultipleConnections"] = maximumMultipeConnections;
            parameters["EncodingType"] = encodingType;
            parameters["BypassProxy"] = bypassProxy;
            parameters["Name"] = user + " at " + host;
            parameters["RemoteDir"] = remoteDir;
            parameters["SyncBrowsing"] = syncBrowsing;
            string container = this.decodeSourceKey(sourceKey)[0].Trim();
            string sourceId = this.decodeSourceKey(sourceKey)[1].Trim();
            this.ftpSources[sourceId] = parameters;
        }
        /// <summary>
        /// Adds the main CSV customer file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="itemIdColumn">The item identifier column.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string addMainCSVCustomerFile(string filePath, string itemIdColumn, string encoding = "UTF-8", string delimiter = ",", string enclosure = "\\&", string escape = "\\\\", string lineSeparator = "\\n", string sourceId = "customers", string container = "customers", bool validate = true)
        {
            string sourceKey = this.addCSVItemFile(filePath, itemIdColumn, encoding, delimiter, enclosure, escape, lineSeparator, sourceId, container, validate);
            this.addSourceIdField(sourceKey, itemIdColumn, null, validate);
            this.addSourceStringField(sourceKey, "bx_customer_id", itemIdColumn, null, validate);
            return sourceKey;
        }

        /// <summary>
        /// Gets the file name from path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="withoutExtension">if set to <c>true</c> [without extension].</param>
        /// <returns></returns>
        public string getFileNameFromPath(string filePath, bool withoutExtension = false)
        {
            string[] parts = filePath.Split('\\');
            string file = parts[parts.Length - 1];
            if (withoutExtension)
            {
                parts = file.Split('.');
                return parts[0].Trim();
            }
            return file;
        }
        /// <summary>
        /// Adds the source identifier field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="col">The col.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceIdField(string sourceKey, object col, string referenceSourceKey = null, bool validate = true)
        {
            
            this.addSourceField(sourceKey, "bx_id", "id", false, col, referenceSourceKey, validate);
        }
        /// <summary>
        /// Decodes the source key.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <returns></returns>
        public String[] decodeSourceKey(string sourceKey)
        {
            return sourceKey.Split('-');
        }

        /// <summary>
        /// Adds the source title field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="colMap">The col map.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceTitleField(string sourceKey, object colMap, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, "bx_title", "title", true, colMap, referenceSourceKey, validate);
        }
        /// <summary>
        /// Adds the source description field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="colMap">The col map.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceDescriptionField(string sourceKey, object colMap, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, "bx_description", "body", true, colMap, referenceSourceKey, validate);
        }

        /// <summary>
        /// Adds the source list price field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="col">The col.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceListPriceField(string sourceKey, string col, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, "bx_listprice", "price", false, col, referenceSourceKey, validate);
        }

        /// <summary>
        /// Adds the source discounted price field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="col">The col.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceDiscountedPriceField(string sourceKey, string col, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, "bx_discountedprice", "discounted", false, col, referenceSourceKey, validate);
        }

        /// <summary>
        /// Adds the source localized text field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="colMap">The col map.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceLocalizedTextField(string sourceKey, string fieldName, object colMap, string referenceSourceKey = null, bool validate = true)
        {
            this.addSourceField(sourceKey, fieldName, "text", true, colMap, referenceSourceKey, validate);
        }

        /// <summary>
        /// Adds the source field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="type">The type.</param>
        /// <param name="localized">if set to <c>true</c> [localized].</param>
        /// <param name="colMap">The col map.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void addSourceField(string sourceKey, string fieldName, string type, bool localized, object colMap, string referenceSourceKey = null, bool validate = true)
        {

            string container = this.decodeSourceKey(sourceKey)[0].Trim();
            string sourceId = this.decodeSourceKey(sourceKey)[1].Trim();

            if (string.IsNullOrEmpty(this.sources[container][sourceId]["fields"].ToString()))
            {
                this.sources[container][sourceId]["fields"] = new NestedDictionary<string, object>();
            }
            this.sources[container][sourceId]["fields"][fieldName]["type"].Value = type;
            this.sources[container][sourceId]["fields"][fieldName]["localized"].Value = localized;
            this.sources[container][sourceId]["fields"][fieldName]["map"].Value = colMap;
            this.sources[container][sourceId]["fields"][fieldName]["referenceSourceKey"].Value = referenceSourceKey;

            Dictionary<string, object> source = (Dictionary<string, object>)this.sources[container][sourceId].Value;

            if (source["format"].ToString() == "CSV")
            {
                if (localized && referenceSourceKey == null)
                {
                    try
                    {
                        Dictionary<string, string> temp_colMap = (Dictionary<string, string>)colMap;


                        foreach (var lang in this.getLanguages())
                        {
                            if (temp_colMap[lang] == null)
                            {
                                throw new BoxalinoException(fieldName + " : no language column provided for language " + lang + " in provided column map): " + temp_colMap.Keys);
                            }


                            if (temp_colMap[lang] is string)
                            {
                            }
                            else
                            {
                                throw new BoxalinoException(fieldName + " : invalid column field name for a non-localized field (expect a string): " + temp_colMap.Keys);
                            }

                            if (validate)
                            {
                                this.validateColumnExistance(container, sourceId, temp_colMap[lang]);
                            }
                        }
                    }
                    catch
                    {
                        throw new BoxalinoException(fieldName + " : invalid column field name for a localized field (expect an array with a column name for each language array(lang=>colName)): " + Convert.ToString(colMap));
                    }

                }
                else
                {
                    if (Convert.ToString(colMap) == null)
                    {
                        throw new BoxalinoException(fieldName + ": invalid column field name for a non-localized field (expect a string): " + Convert.ToString(colMap));
                    }
                    if (validate)
                    {
                        this.validateColumnExistance(container, sourceId, colMap);
                    }
                }
            }
        }

        /// <summary>
        /// Validates the column existance.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="col">The col.</param>
        /// <exception cref="BoxalinoException">the source " + sourceId + " in the container " + container + " declares an column " + col + " which is not present in the header row of the provided CSV file: " + String.Join(",", row)</exception>
        public void validateColumnExistance(string container, string sourceId, object col)
        {
            object rows = getSourceCSVRow(container, sourceId, 0);
            

            int in_array = 0;
            if (rows != null)
            {
                string[] row = Array.ConvertAll<object, string>((object[])rows, Convert.ToString);
                foreach (string item in row)
                {
                    if (item == Convert.ToString(col))
                    {
                        in_array++;
                        break;
                    }
                }
                if (in_array == 0)
                {
                    throw new BoxalinoException("the source " + sourceId + " in the container " + container + " declares an column " + col + " which is not present in the header row of the provided CSV file: " + String.Join(",", row));
                }
            }

        }

        /// <summary>
        /// Gets the source CSV row.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="row">The row.</param>
        /// <param name="maxRow">The maximum row.</param>
        /// <returns></returns>
        public object getSourceCSVRow(string container, string sourceId, string row = "0", int maxRow = 2)
        {
            NestedDictionary<string, object> sources_value = (NestedDictionary<string, object>)sources[container];
            NestedDictionary<string, object> sources_a_value = (NestedDictionary<string, object>)sources_value[sourceId];

            var reader = new StreamReader(File.OpenRead(Convert.ToString(sources_a_value["filePath"])));
            List<string> list = new List<string>();
            int count = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                sources_a_value["rows"].Value = line;
                sources[container] = sources_a_value;
                if (count++ >= maxRow)
                {
                    break;
                }
            }
            NestedDictionary<string, object> sources_b_value = (NestedDictionary<string, object>)sources_a_value["rows"];

            if (sources_b_value[row] != null)
            {
                return sources_b_value[row];

            }
            return null;
        }
        /// <summary>
        /// Adds the category file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="categoryIdColumn">The category identifier column.</param>
        /// <param name="parentIdColumn">The parent identifier column.</param>
        /// <param name="categoryLabelColumns">The category label columns.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string addCategoryFile(string filePath, string categoryIdColumn, string parentIdColumn, Dictionary<string, object> categoryLabelColumns, string encoding = "UTF-8", string delimiter = ",", string enclosure = "&", string escape = "\\\\", string lineSeparator = "\\n", string sourceId = "resource_categories", string container = "products", bool validate = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "referenceIdColumn", categoryIdColumn }, { "parentIdColumn", parentIdColumn }, { "labelColumns", categoryLabelColumns }, { "encoding", encoding }, { "delimiter", delimiter }, { "enclosure", enclosure }, { "escape", escape }, { "lineSeparator", lineSeparator } };
            return this.addSourceFile(filePath, sourceId, container, "hierarchical", "CSV", parameters, validate);
        }
        /// <summary>
        /// Adds the CSV item file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="itemIdColumn">The item identifier column.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string addCSVItemFile(string filePath, string itemIdColumn, string encoding = "UTF-8", string delimiter = ",", string enclosure = "&", string escape = "\\\\", string lineSeparator = "\\n", string sourceId = null, string container = "products", bool validate = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "itemIdColumn", itemIdColumn }, { "encoding", encoding }, { "delimiter", delimiter }, { "enclosure", enclosure }, { "escape", escape }, { "lineSeparator", lineSeparator } };
            if (sourceId == null)
            {
                sourceId = this.getFileNameFromPath(filePath, true);
            }
            return this.addSourceFile(filePath, sourceId, container, "item_data_file", "CSV", parameters, validate);
        }

        /// <summary>
        /// Adds the resource file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="categoryIdColumn">The category identifier column.</param>
        /// <param name="labelColumns">The label columns.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string addResourceFile(string filePath, string categoryIdColumn, object labelColumns, string encoding = "UTF-8", string delimiter = ",", string enclosure = "\\&", string escape = "\\\\", string lineSeparator = "\\n", string sourceId = null, string container = "products", bool validate = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
            { "referenceIdColumn", categoryIdColumn },
            { "labelColumns", labelColumns },
            { "encoding", encoding },
            { "delimiter", delimiter },
            { "enclosure", enclosure },
            { "escape", escape },
            { "lineSeparator", lineSeparator }
            };

            if (sourceId == null)
            {
                sourceId = "resource_" + this.getFileNameFromPath(filePath, true);
            }
            return this.addSourceFile(filePath, sourceId, container, "resource", "CSV", parameters, validate);
        }

        /// <summary>
        /// Sets the CSV transaction file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="orderIdColumn">The order identifier column.</param>
        /// <param name="productIdColumn">The product identifier column.</param>
        /// <param name="customerIdColumn">The customer identifier column.</param>
        /// <param name="orderDateIdColumn">The order date identifier column.</param>
        /// <param name="totalOrderValueColumn">The total order value column.</param>
        /// <param name="productListPriceColumn">The product list price column.</param>
        /// <param name="productDiscountedPriceColumn">The product discounted price column.</param>
        /// <param name="productIdField">The product identifier field.</param>
        /// <param name="customerIdField">The customer identifier field.</param>
        /// <param name="productsContainer">The products container.</param>
        /// <param name="customersContainer">The customers container.</param>
        /// <param name="format">The format.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string setCSVTransactionFile(string filePath, string orderIdColumn, string productIdColumn, string customerIdColumn, string orderDateIdColumn, string totalOrderValueColumn, string productListPriceColumn, string productDiscountedPriceColumn, string productIdField = "bx_item_id", string customerIdField = "bx_customer_id", string productsContainer = "products", string customersContainer = "customers", string format = "CSV", string encoding = "UTF-8", string delimiter = ",", string enclosure = "\\&", string escape = "\\\\", string lineSeparator = "\\n", string container = "transactions", string sourceId = "transactions", bool validate = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
          {
            { "encoding", encoding },
            { "delimiter", delimiter },
            { "enclosure", enclosure },
            { "escape", escape },
            { "lineSeparator", lineSeparator }
            };

            parameters["file"] = this.getFileNameFromPath(filePath);
            parameters["orderIdColumn"] = orderIdColumn;
            parameters["productIdColumn"] = productIdColumn;
            parameters["product_property_id"] = productIdField;
            parameters["customerIdColumn"] = customerIdColumn;
            parameters["customer_property_id"] = customerIdField;
            parameters["productListPriceColumn"] = productListPriceColumn;
            parameters["productDiscountedPriceColumn"] = productDiscountedPriceColumn;
            parameters["totalOrderValueColumn"] = totalOrderValueColumn;
            parameters["orderReceptionDateColumn"] = orderDateIdColumn;

            return this.addSourceFile(filePath, sourceId, container, "transactions", format, parameters, validate);
        }

        /// <summary>
        /// Adds the CSV customer file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="itemIdColumn">The item identifier column.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="enclosure">The enclosure.</param>
        /// <param name="escape">The escape.</param>
        /// <param name="lineSeparator">The line separator.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        public string addCSVCustomerFile(string filePath, string itemIdColumn, string encoding = "UTF-8", string delimiter = ",", string enclosure = "\\&", string escape = "\\\\", string lineSeparator = "\\n", string sourceId = null, string container = "customers", bool validate = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "itemIdColumn", itemIdColumn }, { "encoding", encoding }, { "delimiter", delimiter }, { "enclosure", enclosure }, { "escape", escape }, { "lineSeparator", lineSeparator } };

            if (sourceId == null)
            {
                sourceId = this.getFileNameFromPath(filePath, true);
            }
            return this.addSourceFile(filePath, sourceId, container, "item_data_file", "CSV", parameters, validate);
        }
        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns></returns>
        public string[] getLanguages()
        {
            return this.languages;
        }
        /// <summary>
        /// Sets the languages.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public void setLanguages(string[] languages)
        {
            this.languages = languages;
        }
        /// <summary>
        /// Sets the category field.
        /// </summary>
        /// <param name="sourceKey">The source key.</param>
        /// <param name="col">The col.</param>
        /// <param name="referenceSourceKey">The reference source key.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        public void setCategoryField(string sourceKey, string col, string referenceSourceKey = "resource_categories", bool validate = true)
        {
            if (referenceSourceKey == "resource_categories")
            {
                string container = decodeSourceKey(sourceKey)[0].Trim();
                string sourceId = decodeSourceKey(sourceKey)[1].Trim();
                referenceSourceKey = this.encodesourceKey(container, referenceSourceKey);
            }
            this.addSourceField(sourceKey, "category", "hierarchical", false, col, referenceSourceKey, validate);

        }
        /// <summary>
        /// Adds the source file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="type">The type.</param>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">trying to add a source before having declared the languages with method setLanguages</exception>
        public string addSourceFile(string filePath, string sourceId, string container, string type, string format = "CSV", object parameters = null, bool validate = true)
        {
            if (this.getLanguages().Length == 0)
            {
                throw new BoxalinoException("trying to add a source before having declared the languages with method setLanguages");
            }
            if (this.sources.Count == 0)
            {
                this.sources.Add(container, new NestedDictionary<string, object> { });
            }
            Dictionary<string, object> temp_parameters = (Dictionary<string, object>)parameters;
            temp_parameters["filePath"] = filePath;
            temp_parameters["format"] = format;
            temp_parameters["type"] = type;

           
            this.sources[container][sourceId].Value = temp_parameters;

            if (validate)
            {
                this.validateSource(container, sourceId);
            }
            this.sourceIdContainers[sourceId] = container;
            return this.encodesourceKey(container, sourceId);
        }

        /// <summary>
        /// Encodesources the key.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns></returns>
        public string encodesourceKey(string container, string sourceId)
        {
            return container + " -" + sourceId;
        }


        /// <summary>
        /// Validates the source.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        public void validateSource(string container, string sourceId)
        {
            try
            {
                

                Dictionary<string, object> source = (Dictionary<string, object>)this.sources[container][sourceId].Value;

                

                if (source["format"].ToString() == "CSV")
                {
                    if (source["itemIdColumn"] != null)
                    {
                        this.validateColumnExistance(container, sourceId, source["itemIdColumn"]);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Validates the column existance.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="col">The col.</param>
        /// <exception cref="BoxalinoException">the source " + sourceId + " in the container " + container + " declares an column " + col + " which is not present in the header row of the provided CSV file: " + String.Join(",", row)</exception>
        public void validateColumnExistance(string container, string sourceId, string col)
        {
           

            object rows = this.getSourceCSVRow(container, sourceId, 0);

            int in_array = 0;
            if (rows != null)
            {
                string[] row = Array.ConvertAll<object, string>((object[])rows, Convert.ToString);
                foreach (string item in row)
                {
                    if (item == Convert.ToString(col))
                    {
                        in_array++;
                        break;
                    }
                }
                if (in_array == 0)
                {
                    throw new BoxalinoException("the source " + sourceId + " in the container " + container + " declares an column " + col + " which is not present in the header row of the provided CSV file: " + String.Join(",", row));
                }
            }
        }

        /// <summary>
        /// Gets the source CSV row.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="row">The row.</param>
        /// <param name="maxRow">The maximum row.</param>
        /// <returns></returns>
        public object getSourceCSVRow(string container, string sourceId, int row = 0, int maxRow = 2)
        {
            try
            {
               
                Dictionary<string, object> source = (Dictionary<string, object>)this.sources[container][sourceId].Value;

                source["rows"] = new NestedDictionary<string, object>();
                if (source["rows"] != null)
                {
                    int count = 1;
                    var reader = new StreamReader(File.OpenRead(Convert.ToString(source["filePath"])));
                    List<string> listA = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        listA.Add(values[0]);
                        if (count++ >= maxRow)
                        {
                            break;
                        }
                    }
                    source["rows"] = listA[0].Split(',');
                  
                }
                if (source["rows"] != null)
                {
                    return source["rows"];
                }
                return null;
            }
            catch { return null; }
        }

        /// <summary>
        /// Gets the XML.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">source parameter " + defaultValue.Key + " required but not defined in source id " + sourceId + " for container '$containerName'</exception>
        public XmlDocument getXML()
        {
            var xmlDocument = new XmlDocument();

            var xmlNode = xmlDocument.CreateNode(XmlNodeType.XmlDeclaration, "root", "namespace");
            xmlDocument.AppendChild(xmlNode);

            XmlElement rootXML = xmlDocument.CreateElement("", "root", "");
            xmlDocument.AppendChild(rootXML);
           
            XmlElement languagesXML = xmlDocument.CreateElement("", "languages", "");
            XmlNode languages = rootXML.AppendChild(languagesXML);

            foreach (var lang in this.getLanguages())
            {
                XmlElement languageXML = xmlDocument.CreateElement("", "language", "");
                XmlAttribute a = xmlDocument.CreateAttribute("id");
                a.Value = lang;
                languageXML.Attributes.Append(a);
                XmlNode language = languagesXML.AppendChild(languageXML);

            }
         
            XmlElement containersXML = xmlDocument.CreateElement("", "containers", "");
            rootXML.AppendChild(containersXML);
            foreach (var containerSources in this.sources)
            {
               
                foreach (var sourceValues in (NestedDictionary<string, object>)containerSources.Value)
                {
                    XmlElement containerXML = xmlDocument.CreateElement("", "container", "");
                    XmlNode container = containersXML.AppendChild(containerXML);
                    XmlAttribute container_id = xmlDocument.CreateAttribute("id");
                    container_id.Value = Convert.ToString(containerSources.Key);
                    XmlAttribute containertype = xmlDocument.CreateAttribute("type");
                    containertype.Value = Convert.ToString(containerSources.Key);
                    containerXML.Attributes.Append(container_id);
                    containerXML.Attributes.Append(containertype);

                    XmlElement sourcesXML = xmlDocument.CreateElement("", "sources", "");
                    XmlNode sources = containerXML.AppendChild(sourcesXML);

                    XmlElement propertiesXML = xmlDocument.CreateElement("", "properties", "");
                    XmlNode properties = containerXML.AppendChild(propertiesXML);
                    string sourceId = sourceValues.Key;
                    XmlElement sourceXML = xmlDocument.CreateElement("source");
                    XmlNode source = sourcesXML.AppendChild(sourceXML);
                    XmlAttribute xmlAttribute_source_id = xmlDocument.CreateAttribute("id");
                    xmlAttribute_source_id.Value = sourceId;
                    XmlAttribute xmlAttribute_source_type = xmlDocument.CreateAttribute("type");

                    
                    Dictionary<string, object> temp_sourceValues = (Dictionary<string, object>)this.sources[containerSources.Key][sourceId].Value;
                    xmlAttribute_source_type.Value = temp_sourceValues["type"].ToString();

                   
                    sourceXML.Attributes.Append(xmlAttribute_source_id);
                    sourceXML.Attributes.Append(xmlAttribute_source_type);

                   
                    temp_sourceValues["file"] = this.getFileNameFromPath(Convert.ToString(temp_sourceValues["filePath"]));

                  
                   IDictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters["file"] = false;
                    parameters["format"] = "CSV";
                    parameters["encoding"] = "UTF-8";
                    parameters["delimiter"] = ",";
                    parameters["enclosure"] = "\"";
                    parameters["escape"] = "\\\\";
                    parameters["lineSeparator"] = "\\n";


                    switch (Convert.ToString(temp_sourceValues["type"]))
                    {
                        case "item_data_file":
                            parameters["itemIdColumn"] = false;
                            break;

                        case "hierarchical":
                            parameters["referenceIdColumn"] = false;
                            parameters["parentIdColumn"] = false;
                            parameters["labelColumns"] = false;
                            break;

                        case "resource":
                            parameters["referenceIdColumn"] = false;
                            parameters["itemIdColumn"] = false;
                            parameters["labelColumns"] = false;
                            temp_sourceValues["itemIdColumn"] = temp_sourceValues["referenceIdColumn"];
                            break;

                        case "transactions":
                            parameters.Clear();
                            foreach (var param in temp_sourceValues)
                            {

                               parameters.Add(new KeyValuePair<string, object>(param.Key,param.Value));
                              
                            }
                          
                            
                            parameters.Remove("filePath");
                            parameters.Remove("type");
                            parameters.Remove("product_property_id");
                            parameters.Remove("customer_property_id");
                            
                            break;
                    }
                    XmlNode parameter = null;


                    foreach (var defaultValue in parameters)
                    {
                        object value = temp_sourceValues[defaultValue.Key] != null ? temp_sourceValues[defaultValue.Key] : defaultValue.Value;
                        try
                        {
                            if (string.IsNullOrEmpty(value.ToString()))
                            {
                                throw new BoxalinoException("source parameter " + defaultValue.Key + " required but not defined in source id " + sourceId + " for container '$containerName'");
                            }
                        }
                        catch (BoxalinoException ex)
                        {
                        }

                        XmlElement xml_parameter = xmlDocument.CreateElement(defaultValue.Key);
                        if (parameter != null)
                        {
                            parameter = parameter.AppendChild(xml_parameter);
                          
                        }
                        else
                        {
                            parameter = source.AppendChild(xml_parameter);
                        }


                       
                        if (value is Dictionary<string, object>)
                        {

                          
                                foreach (var lang in this.getLanguages())
                                {
                                    Dictionary<string, object> temp_value = (Dictionary<string, object>)value;

                                    XmlElement languageParamXML = xmlDocument.CreateElement("language");
                                    parameter.AppendChild(languageParamXML);
                                    XmlAttribute language_name = xmlDocument.CreateAttribute("name");
                                    language_name.Value = lang;
                                    XmlAttribute language_value = xmlDocument.CreateAttribute("value");
                                    language_value.Value = temp_value[lang].ToString();
                                    languageParamXML.Attributes.Append(language_name);
                                    languageParamXML.Attributes.Append(language_value);
                                }
                            
                        }
                        else
                        {
                            XmlAttribute valueXML = xmlDocument.CreateAttribute("value");
                            valueXML.Value = Convert.ToString(value);
                            parameter.Attributes.Append(valueXML);
                        }

                        if (temp_sourceValues["type"].ToString() == "transactions")
                        {
                            switch (Convert.ToString(defaultValue.Key))
                            {
                                case "productIdColumn":
                                    XmlAttribute product_property_id_attr = xmlDocument.CreateAttribute("product_property_id");
                                    product_property_id_attr.Value = Convert.ToString(temp_sourceValues["product_property_id"]);
                                    parameter.Attributes.Append(product_property_id_attr);
                                    break;

                                case "customerIdColumn":
                                    XmlAttribute customer_property_id = xmlDocument.CreateAttribute("customer_property_id");
                                    customer_property_id.Value = Convert.ToString(temp_sourceValues["customer_property_id"]);
                                    parameter.Attributes.Append(customer_property_id);

                                    if (temp_sourceValues.ContainsKey("guest_property_id"))
                                    {
                                        XmlAttribute guest_property_id = xmlDocument.CreateAttribute("guest_property_id");
                                        guest_property_id.Value = Convert.ToString(temp_sourceValues["guest_property_id"]);
                                        parameter.Attributes.Append(guest_property_id);
                                    }
                                    break;
                            }
                        }
                    }

                    if (this.ftpSources.Count != 0 && Convert.ToString(this.ftpSources[sourceId]) != null)
                    {
                        XmlElement locationXML = xmlDocument.CreateElement("location");
                        parameter.AppendChild(locationXML);
                        XmlAttribute type = xmlDocument.CreateAttribute("type");
                        type.Value = "ftp";
                        parameter.Attributes.Append(type);
                        XmlElement ftpXML = xmlDocument.CreateElement("ftp");
                        parameter.AppendChild(locationXML);

                        XmlAttribute ftp_attr = xmlDocument.CreateAttribute("name");
                        ftp_attr.Value = "ftp";
                        XmlNode ftp = ftpXML.Attributes.Append(ftp_attr);


                        foreach (var ftpPv in (Dictionary<string, object>)ftpSources[sourceId])
                        {
                            string ftpPn = ftpPv.Key;
                          
                        }
                    }

                   

                    if (this.sources[containerSources.Key][sourceId].ContainsKey("fields"))
                    {
                   
                        foreach (var fieldValues in this.sources[containerSources.Key][sourceId]["fields"])
                        {

                            string fieldId = fieldValues.Key;


                            NestedDictionary<string, object> temp_fieldValues = (NestedDictionary<string, object>)fieldValues.Value;

                            XmlElement propertyXML = xmlDocument.CreateElement("property");
                            propertiesXML.AppendChild(propertyXML);
                            XmlAttribute property_id = xmlDocument.CreateAttribute("id");
                            property_id.Value = fieldId;
                            XmlAttribute property_type = xmlDocument.CreateAttribute("type");
                            property_type.Value = temp_fieldValues["type"].Value.ToString();
                            propertyXML.Attributes.Append(property_id);
                            propertyXML.Attributes.Append(property_type);

                            XmlElement transformXML = xmlDocument.CreateElement("transform");
                            propertyXML.AppendChild(transformXML);

                            XmlElement logicXML = xmlDocument.CreateElement("logic");
                            transformXML.AppendChild(logicXML);                            

                            string referenceSourceKey = (temp_fieldValues["referenceSourceKey"] != null) ? Convert.ToString(temp_fieldValues["referenceSourceKey"].Value) : null;
                            string logicType = referenceSourceKey == null || referenceSourceKey == "" ? "direct" : "reference";
                            if (logicType == "direct")
                            {
                                if (temp_fieldValues.ContainsKey("fieldParameters"))
                                {
                                    foreach (var parameterValue in (NestedDictionary<string, object>)temp_fieldValues["fieldParameters"])
                                    {
                                        string parameterName = parameterValue.Key;
                                        switch (parameterName)
                                        {
                                            case "pc_fields":
                                            case "pc_tables":
                                                logicType = "advanced";
                                                break;
                                        }
                                    }
                                }
                            }
                            XmlAttribute type_attr = xmlDocument.CreateAttribute("type");
                            type_attr.Value = logicType;

                            XmlAttribute logic_attr = xmlDocument.CreateAttribute("source");
                            logic_attr.Value = sourceId;

                            logicXML.Attributes.Append(type_attr);
                            logicXML.Attributes.Append(logic_attr);
                            



                            XmlElement fieldXML = xmlDocument.CreateElement("field");
                            XmlNode field = logicXML.AppendChild(fieldXML);
                            if (temp_fieldValues["map"].Value is Dictionary<string, string>)
                            {
                                foreach (var lang in this.getLanguages())
                                {

                                    XmlAttribute column_attr = xmlDocument.CreateAttribute("column");
                                   
                                    Dictionary<string, string> temp_colMap = (Dictionary<string, string>)temp_fieldValues["map"].Value;
                                    column_attr.Value = temp_colMap[lang];
                                    XmlAttribute language_attr = xmlDocument.CreateAttribute("language");
                                    language_attr.Value = lang;
                                    fieldXML.Attributes.Append(column_attr);
                                    fieldXML.Attributes.Append(language_attr);
                                }
                            }
                            else
                            {
                                XmlAttribute column_attr = xmlDocument.CreateAttribute("column");
                                column_attr.Value = Convert.ToString(temp_fieldValues["map"].Value);
                                fieldXML.Attributes.Append(column_attr);

                            }
                            XmlElement paramsXML = xmlDocument.CreateElement("params");
                            propertyXML.AppendChild(paramsXML);
                           
                            if (referenceSourceKey != "")
                            {
                                XmlElement referenceSourceXML = xmlDocument.CreateElement("referenceSource");

                                paramsXML.AppendChild(referenceSourceXML);
                                string referenceContainer = decodeSourceKey(referenceSourceKey)[0].Trim();
                                string referenceSourceId = decodeSourceKey(referenceSourceKey)[1].Trim();

                                XmlAttribute value_attr = xmlDocument.CreateAttribute("value");
                                value_attr.Value = referenceSourceId;
                                referenceSourceXML.Attributes.Append(value_attr);
                            }
                            if (temp_fieldValues.ContainsKey("fieldParameters"))
                            {
                                foreach (var parameterValue in (NestedDictionary<string, object>)temp_fieldValues["fieldParameters"])
                                {
                                    string parameterName = parameterValue.Key;
                                    XmlElement fieldParameterXML = xmlDocument.CreateElement("fieldParameter");
                                    paramsXML.AppendChild(fieldParameterXML);
                                    XmlAttribute name_attr = xmlDocument.CreateAttribute("name");
                                    XmlAttribute value_attr = xmlDocument.CreateAttribute("value");
                                    name_attr.Value = parameterName;
                                    value_attr.Value = Convert.ToString(temp_fieldValues["fieldParameters"][parameterName].Value);

                                    fieldParameterXML.Attributes.Append(value_attr);
                                    fieldParameterXML.Attributes.Append(name_attr);
                                    

                                }
                            }
                        }
                    }
                }
            }
            return xmlDocument;
        }

        /// <summary>
        /// Pushes the data specifications.
        /// </summary>
        /// <param name="ignoreDeltaException">if set to <c>true</c> [ignore delta exception].</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">You should not push specifications when you are pushing a delta file. Only do it when you are preparing full files. Set method parameter ignoreDeltaException to true to ignore this exception and publish anyway.</exception>
        public Dictionary<string, object> pushDataSpecifications(bool ignoreDeltaException = false)
        {

            if (!ignoreDeltaException && this.isDelta)
            {
                throw new BoxalinoException("You should not push specifications when you are pushing a delta file. Only do it when you are preparing full files. Set method parameter ignoreDeltaException to true to ignore this exception and publish anyway.");
            }

            Dictionary<string, object> fields = new Dictionary<string, object>{
            {"username",this.bxClient.getUsername()},
            {"password",this.bxClient.getPassword()},
            {"account",this.bxClient.getAccount(false)},
            {"owner",this.owner},
            {"xml",this.getXML()}
        };
            string url = this.host + URL_XML;
            return callAPI(fields, url);
        }

        /// <summary>
        /// Checks the changes.
        /// </summary>
        public void checkChanges()
        {
            this.publishOwnerChanges(false);
        }

        /// <summary>
        /// Calls the API.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="url">The URL.</param>
        /// <param name="temporaryFilePath">The temporary file path.</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">Error Occurred :" + myException.Message</exception>
        protected Dictionary<string, object> callAPI(Dictionary<string, object> fields, string url, string temporaryFilePath = null)
        {

            string responseFromServer = string.Empty;
            try
            {
               
                WebRequest request = WebRequest.Create(url);
               
                request.Method = "POST";
           
                string postData = string.Empty;
                foreach (var item in fields)
                {
                    postData += HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value.GetType() == typeof(String) ? item.Value.ToString() : ((XmlDocument)item.Value).InnerXml) + "&";                    
                }
                if (postData.EndsWith("&"))
                {
                    postData = postData.Substring(0, postData.Length - 1);
                }
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
             
                request.ContentType = "application/x-www-form-urlencoded";
              
                request.ContentLength = byteArray.Length;
              
                Stream dataStream = request.GetRequestStream();
              
                dataStream.Write(byteArray, 0, byteArray.Length);
               
                dataStream.Close();
              
                WebResponse response = request.GetResponse();

                dataStream = response.GetResponseStream();
               
                StreamReader reader = new StreamReader(dataStream);
             
                responseFromServer = reader.ReadToEnd();

               
                reader.Close();
                dataStream.Close();
                response.Close();

            }
            catch (BoxalinoException myException)
            {
                throw new BoxalinoException("Error Occurred :" + myException.Message);
            }

            return this.checkResponseBody(responseFromServer, url);
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <param name="responseBody">The response body.</param>
        /// <returns></returns>
        public string getError(string responseBody)
        {
            return responseBody;
        }
        /// <summary>
        /// Publishes the changes.
        /// </summary>
        public void publishChanges()
        {
            this.publishOwnerChanges(true);
        }

        /// <summary>
        /// Publishes the owner changes.
        /// </summary>
        /// <param name="publish">if set to <c>true</c> [publish].</param>
        /// <returns></returns>
        public Dictionary<string, object> publishOwnerChanges(bool publish = true)
        {
            if (this.isDev)
            {
                publish = false;
            }
            Dictionary<string, object> fields = new Dictionary<string, object>
            {
            {"username",this.bxClient.getUsername()},
            {"password",this.bxClient.getPassword()},
            {"account",this.bxClient.getAccount(false)},
            {"owner",this.owner},
            {"publish",publish ? "true" : "false"}
            };
            string url = this.host + URL_PUBLISH_CONFIGURATION_CHANGES;
            return this.callAPI(fields, url);
        }
        /// <summary>
        /// Verifies the credentials.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> verifyCredentials()
        {
            Dictionary<string, object> fields = new Dictionary<string, object>
            {
                {"username",this.bxClient.getUsername()},
                {"password",this.bxClient.getPassword()},
                {"account",this.bxClient.getAccount(false)},
                {"owner",this.owner}
            };

            string url = this.host + URL_VERIFY_CREDENTIALS;
            return this.callAPI(fields, url);
        }

        /// <summary>
        /// Checks the response body.
        /// </summary>
        /// <param name="responseBody">The response body.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">
        /// API response of call to " + url + " is empty string, this is an error!
        /// or
        /// </exception>
        public Dictionary<string, object> checkResponseBody(string responseBody, string url)
        {
            if (responseBody == null)
            {
                throw new BoxalinoException("API response of call to " + url + " is empty string, this is an error!");
            }


            Dictionary<string, object> value = (Dictionary<string, object>)JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
            if (value.ContainsKey("token"))
            {
                if (value.ContainsKey("changes"))
                {
                    if (value["changes"].ToString().Count() > 2)
                    {
                        throw new BoxalinoException(responseBody);
                    }
                }
            }
            return value;
        }


        /// <summary>
        /// Pushes the data.
        /// </summary>
        /// <param name="temporaryFilePath">The temporary file path.</param>
        /// <returns></returns>
        public Dictionary<string, object> pushData(string temporaryFilePath = null)
        {

            string zipFile = this.createZip(temporaryFilePath);

            Dictionary<string, object> fields = new Dictionary<string, object>
                {
                {"username",this.bxClient.getUsername()},
                {"password",this.bxClient.getPassword()},
                {"account",this.bxClient.getAccount(false)},
                {"owner" ,this.owner},
                {"dev",this.isDev ? "true" : "false"},
                {"delta",this.isDelta ? "true" : "false"},
                {"data" ,this.getCurlFile(zipFile, "application/zip")}
                };
            string url = this.host + URL_ZIP;
            return this.callAPI(fields, url, temporaryFilePath);
        }
        /// <summary>
        /// Gets the curl file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected string getCurlFile(string filename, string type) 
        {
           

            return filename;
        }

        /// <summary>
        /// Gets the task execute URL.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns></returns>
        public string getTaskExecuteUrl(string taskName)
        {
            return this.host + URL_EXECUTE_TASK + "?iframeAccount=" + this.bxClient.getAccount() + "&task_process=" + taskName;
        }

        /// <summary>
        /// Publishes the choices.
        /// </summary>
        /// <param name="isTest">if set to <c>true</c> [is test].</param>
        /// <param name="taskName">Name of the task.</param>
        public void publishChoices(bool isTest = false, string taskName = "generate_optimization")
        {

            if (this.isDev)
            {
                taskName += "_dev";
            }
            if (isTest)
            {
                taskName += "_test";
            }
            string url = this.getTaskExecuteUrl(taskName);
            Common.file_get_contents(url);
        }

        /// <summary>
        /// Prepares the index of the corpus.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        public void prepareCorpusIndex(string taskName = "corpus")
        {
            string url = this.getTaskExecuteUrl(taskName);
            Common.file_get_contents(url);
        }

        /// <summary>
        /// Prepares the index of the autocomplete.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="taskName">Name of the task.</param>
        public void prepareAutocompleteIndex(object fields, string taskName = "autocomplete")
        {
            string url = this.getTaskExecuteUrl(taskName);
            Common.file_get_contents(url);
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> getFiles()
        {
            Dictionary<string, object> files = new Dictionary<string, object>();
            foreach (var containerSources in this.sources)
            {
                string container = containerSources.Key;
                NestedDictionary<string, object> temp_containerSources = (NestedDictionary<string, object>)containerSources.Value;
                foreach (var sourceValues in temp_containerSources)
                {
                    string sourceId = sourceValues.Key;
                    

                    Dictionary<string, object> temp_source = (Dictionary<string, object>)this.sources[container][sourceId].Value;

                    if (temp_source.ContainsKey(sourceId))
                    {
                        continue;
                    }
                    if (temp_source.ContainsKey("file"))
                    {
                        temp_source["file"] = this.getFileNameFromPath(Convert.ToString(temp_source["filePath"]));

                      
                    }
                    files[Convert.ToString(temp_source["file"])] = temp_source["filePath"];
                }
            }
            return files;
        }

        /// <summary>
        /// Creates the zip.
        /// </summary>
        /// <param name="temporaryFilePath">The temporary file path.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">Synchronization failure: Failed to add file " +
        ///                            filePath.Key + " to the zip " +
        ///                            name + ". Please try again.</exception>
        public string createZip(string temporaryFilePath = null, string name = "bxdata.zip")
        {
            if (temporaryFilePath == null)
            {
                temporaryFilePath = System.IO.Path.GetTempPath() + "bxclient";
            }

            if (temporaryFilePath != "" && !System.IO.File.Exists(temporaryFilePath))
            {

                Directory.CreateDirectory(temporaryFilePath);
            }

            string zipFilePath = temporaryFilePath + "\\" + name;

            if (System.IO.File.Exists(zipFilePath))
            {
                Common.unlink(zipFilePath);
            }

            Dictionary<string, object> files = this.getFiles();

            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive zip = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    foreach (var filePath in files)
                    {
                        try
                        {
                            ZipArchiveEntry demoFile = zip.CreateEntry(Convert.ToString(filePath.Key));

                           

                            using (var memoryStream = new MemoryStream())
                            {
                                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                                {
                                    using (var entryStream = demoFile.Open())
                                  


                                    using (var fileStream = new FileStream(Convert.ToString(filePath.Value), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                    {
                                        using (StreamReader streamReader = new StreamReader(fileStream))
                                        {
                                          
                                            using (var streamWriter = new StreamWriter(entryStream))
                                            {
                                                streamWriter.Write(streamReader.ReadToEnd());
                                            }
                                        }

                                    }
                                }
                            }

                        }
                        catch
                        {
                            throw new BoxalinoException(
                           "Synchronization failure: Failed to add file " +
                           filePath.Key + " to the zip " +
                           name + ". Please try again.");
                        }





                    }
                    ZipArchiveEntry readmeEntry1 = zip.CreateEntry("properties.xml");



                    using (StreamWriter writer = new StreamWriter(readmeEntry1.Open()))
                    {
                        using (var stringWriter = new StringWriter())
                        using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                        {
                            getXML().WriteTo(xmlTextWriter);
                            xmlTextWriter.Flush();
                            writer.WriteLine(stringWriter.GetStringBuilder().ToString());
                        }

                    }

                }
            }

            return zipFilePath;
        }

    }
}
