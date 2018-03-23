using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BankClearFileCopy
{
    /// <summary>
    /// 边界类，管理对象
    /// </summary>
    public class Manager
    {
        DateTime _dtNow;                    // 处理日期
        private static Manager _instance;   // 单例模式
        private BankDistCollection _bankDistCollection;     // 存管列表集合对象（类似列表对象）

        private bool _isRunning;            // 是否运行中

        /// <summary>
        /// 构造函数
        /// </summary>
        private Manager()
        {
            _dtNow = DateTime.Now;                              // 日期
            _bankDistCollection = new BankDistCollection();     // 新建collection对象

            // 判断配置文件是否存在，不存在抛出异常
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "cfg.xml")))
                throw new Exception("未能找到配置文件cfg.xml，请重新配置该文件后重启程序!");

            // 读取配置文件
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;                                     //忽略文档里面的注释
            using (XmlReader reader = XmlReader.Create(@"cfg.xml", settings))
            {
                doc.Load(reader);

                // 检查根节点
                XmlNode rootNode = doc.SelectSingleNode("Config");   // 根节点
                if (rootNode == null)
                    throw new Exception("无法找到根配置节点<Config>，请检查配置文件格式是否正确!");


                // 获取存管列表
                XmlNode bankListXN = rootNode.SelectSingleNode("BankList");
                if (bankListXN == null)
                    throw new Exception("无法找到文件列表节点<BankList>，请检查配置文件格式是否正确!");

                XmlNodeList bankXNL = bankListXN.SelectNodes("Bank");
                foreach (XmlNode bankXN in bankXNL)
                {
                    // 临时变量
                    string name = string.Empty;
                    string source = string.Empty;
                    string dest = string.Empty;


                    XmlNode valueXN;    // 临时变量
                    valueXN = bankXN.SelectSingleNode("Name");
                    if (valueXN != null)
                        name = valueXN.InnerText.Trim();

                    valueXN = bankXN.SelectSingleNode("Source");
                    if (valueXN != null)
                    {
                        source = valueXN.InnerText.Trim();
                        source = Util.ReplaceStringWithDateFormat(source, _dtNow);
                    }

                    valueXN = bankXN.SelectSingleNode("Dest");
                    if (valueXN != null)
                    {
                        dest = valueXN.InnerText.Trim();
                        dest = Util.ReplaceStringWithDateFormat(dest, _dtNow);
                    }


                    // 生成配置对象
                    BankDist tmpBankDist = new BankDist(name, source, dest);
                    _bankDistCollection.Add(tmpBankDist);
                }//eof foreach
            }//eof using
        }


        /// <summary>
        /// 单例模式返回对象
        /// </summary>
        /// <returns></returns>
        public static Manager GetInstance()
        {
            if (_instance == null)
                _instance = new Manager();

            return _instance;
        }

        /// <summary>
        /// 获取存管列表对象
        /// </summary>
        public BankDistCollection BankDistCollection
        {
            get { return _bankDistCollection; }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DTNow
        {
            get { return _dtNow; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }
    }
}
