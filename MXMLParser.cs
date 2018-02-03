using System.Xml;
public class MXMLParser
{

    private List<xmlnode> xmlNodes;

    public string xmlString;

    class attribute
    {

        private string name;

        private string value;

        public string getName()
        {
            return name;
        }

        public string getValue()
        {
            return value;
        }

        public void setName(string nm)
        {
            name = nm;
        }

        public void setValue(string v)
        {
            value = v;
        }
    }

    class xmlnode
    {

        private string elementName = "";

        private string elementText = "";

        public ArrayList attributtes = new ArrayList();

        public string getName()
        {
            return elementName;
        }

        public string getText()
        {
            return elementText;
        }

        public attribute getAttribute(string nam)
        {
            foreach (attribute s in attributtes)
            {
                if ((s.getName == nam))
                {
                    getAttribute = s;
                    break;
                }

            }

            return null;
        }

        public void setName(string nam)
        {
            elementName = nam;
        }

        public void setText(string nam)
        {
            elementText = nam;
        }

        public void setAttributes(ArrayList nam)
        {
            attributtes = nam;
        }
    }

    public ArrayList read(string url)
    {
        ArrayList nodes = new ArrayList();
        XmlTextReader reader = new XmlTextReader(url);
        xmlnode xm = new xmlnode();
        xmlString = "";
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    // Display beginning of element.
                    xm.setName(reader.Name);
                    xmlString = (xmlString + ("<" + reader.Name));
                    ArrayList attrs = new ArrayList();
                    if (reader.HasAttributes)
                    {
                        // If attributes exist
                        while (reader.MoveToNextAttribute())
                        {
                            attribute att = new attribute();
                            att.setName(reader.Name);
                            xmlString = (xmlString + (" "
                                        + (reader.Name + ("=\'"
                                        + (reader.Value + "\'")))));
                            att.setValue(reader.Value);
                            attrs.Add(att);
                        }

                    }

                    xmlString += ">";
                    xm.setAttributes(attrs);
                    break;
                case XmlNodeType.Text:
                    xmlString = (xmlString + reader.Value);
                    xm.setText(reader.Value);
                    break;
                case XmlNodeType.CDATA:
                    xmlString = (xmlString + ("<![CDATA["
                                + (reader.Value + "]]>")));
                    xm.setText(reader.Value);
                    break;
                case XmlNodeType.EndElement:
                    xmlString = (xmlString + ("</"
                                + (reader.Name + ">")));
                    nodes.Add(xm);
                    xm = new xmlnode();
                    break;
            }
        }

        return nodes;
    }
}