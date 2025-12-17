Word Quest 🧩

Word Quest is a Wordle-style game developed as a personal project, focused on a flexible architecture, clear visual feedback, and full multi-language support. The goal of this project is to build a solid and scalable foundation for word games, allowing systems to be easily reused and extended.

🎮 Overview

Word Quest challenges players to guess a hidden word within a limited number of attempts. The game provides immediate feedback through colors, animations, and popups, reinforcing player actions and ensuring clear communication at all times.

The project is designed to be configurable, localizable, and expandable.

✨ Main Features
📚 Dynamic word system

Words are loaded from text files.

Support for different word lengths:

5 letters

6 letters

7 letters

Word lists can be modified or expanded without changing code.

🌍 Dynamic localization

Fully dynamic localization system.

Supported languages:

🇬🇧 English

🇪🇸 Spanish

🇧🇷 Portuguese

Works with both static UI texts and texts generated via code.

🎨 Letter color feedback

Color-based feedback system to indicate letter states:

Correct letter in the correct position

Correct letter in the wrong position

Letter not present in the word

Clear and consistent visual feedback to improve readability.

🎬 Animations

Animations for letters and rows when submitting words.

Error and confirmation animations.

Animations are used as visual reinforcement for game feedback.

💬 Popup feedback system

Reusable popup system to display feedback messages such as:

Invalid word

Errors

Informational messages

Controlled entirely via code, with auto-hide behavior.

📊 Game statistics

Game statistics tracking system.

Designed to be easily expandable with additional data (streaks, games played, etc.).

🧱 Architecture & Technical Approach

Event-driven architecture to decouple systems and improve scalability.

Modular and maintainable codebase.

Clear separation between game logic, UI, and visual feedback systems.

Systems designed to be reusable across other projects.

🚀 Project Goals

Practice and refine word game mechanics.

Implement reusable systems (localization, feedback, animations).

Build a solid technical foundation for future iterations or similar games.

📌 Possible Future Improvements

Additional game modes.

More supported languages.

Persistent saving of statistics.

Configurable difficulty levels.

Leaderboards or ranking integration.

🛠️ Technologies

Engine: Unity

Language: C#

UI: Unity UI System

📄 License

This project is intended for educational and experimental purposes.
