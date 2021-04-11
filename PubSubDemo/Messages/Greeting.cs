using System;

namespace Messages {
	public class Greeting {
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }

        public Greeting(string text) {
            this.Text = text;
            this.Date = DateTimeOffset.UtcNow;
        }
	}
}
