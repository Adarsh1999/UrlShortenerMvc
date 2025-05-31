async function testClicks(shortUrl) {
    const userAgents = [
        'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36',
        'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Safari/605.1.15',
        'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0',
        'Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1'
    ];

    const referrers = [
        'https://www.google.com',
        'https://www.facebook.com',
        'https://www.twitter.com',
        'https://www.linkedin.com',
        'https://www.reddit.com'
    ];

    console.log('Starting click simulation...');

    for (let i = 0; i < 5; i++) {
        try {
            const response = await fetch(shortUrl, {
                method: 'GET',
                headers: {
                    'User-Agent': userAgents[i % userAgents.length],
                    'Referer': referrers[i % referrers.length]
                }
            });

            console.log(`Click ${i + 1} completed with status: ${response.status}`);

            // Wait for 1 second between clicks
            await new Promise(resolve => setTimeout(resolve, 1000));
        } catch (error) {
            console.error(`Error on click ${i + 1}:`, error);
        }
    }

    console.log('Click simulation completed!');
    console.log('Please refresh the stats page to see the new clicks.');
}

// Add a button to the stats page
document.addEventListener('DOMContentLoaded', function () {
    const statsContainer = document.querySelector('.card-body');
    if (statsContainer) {
        const testButton = document.createElement('button');
        testButton.className = 'btn btn-primary mt-3';
        testButton.textContent = 'Simulate 5 Clicks';
        testButton.onclick = function () {
            const shortUrl = document.querySelector('input[readonly]').value;
            testClicks(shortUrl);
        };
        statsContainer.appendChild(testButton);
    }
}); 