using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatSharp
{
    public class ChannelCollection : IEnumerable<IrcChannel>
    {
        internal ChannelCollection(IrcClient client)
        {
            Channels = new Dictionary<string, IrcChannel>();
            Client = client;
        }

        private IrcClient Client { get; set; }
        private Dictionary<string, IrcChannel> Channels { get; set; }

        internal void Add(IrcChannel channel)
        {
            if (Channels.ContainsKey(channel.Name))
                throw new InvalidOperationException("That channel already exists in this collection.");

            Channels.Add(channel.Name, channel);
        }

        internal void Remove(IrcChannel channel)
        {
            Channels.Remove(channel.Name);
        }

        public void Join(string name)
        {
            Client.JoinChannel(name);
        }

        public bool Contains(string name)
        {
            return Channels.ContainsKey(name);
        }
        
        public IrcChannel this[string name]
        {
            get
            {
                if (!Channels.ContainsKey(name))
                    throw new KeyNotFoundException();

                return Channels[name];
            }
        }

        public IEnumerator<IrcChannel> GetEnumerator()
        {
            return Channels.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
