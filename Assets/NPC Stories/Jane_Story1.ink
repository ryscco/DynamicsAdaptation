EXTERNAL receiveItem(i)
- Jane looks at you hesitantly.
*   'I have a gift for you.'
    Jane: 'Is it from John?'
    * *     'Yes.'
            * * *   Give Jane the gift.
                    'John is so wonderful.'
                    ~ receiveItem("Flower")
            * * *   'I've changed my mind.'
                    Jane: 'I understand.'
    * *     'No.'
            Jane: 'It's from you then, how thoughtful!'
*   'Nevermind.'
- -> END
=== function receiveItem(i) ===
~ return