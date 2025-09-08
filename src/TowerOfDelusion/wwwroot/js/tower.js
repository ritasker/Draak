const StoreName = 'towerOfDelusion';

function gameTerminal() {
    Alpine.store(StoreName, {
        state: {
            currentInput: '',
            terminalHistory: [],
            currentRoom: {},
            playerScore: 246,
            isTyping: false,
            typingText: '',
            apiBaseUrl: '/api',
        },
        init() {

        }
    })
}
