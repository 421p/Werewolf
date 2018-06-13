using System.Collections.Generic;

namespace Werewolf_Control.Models
{
    internal class SpamDetector
    {
        public bool NotifiedAdmin { get; set; } = false;
        public int Warns { get; set; } = 0;
        public HashSet<UserMessage> Messages { get; set; } = new HashSet<UserMessage>();
    }
}