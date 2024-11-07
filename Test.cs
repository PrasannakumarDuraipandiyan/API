async function downloadReport() {
  const url = 'https://example.com/api/download-report';
  const data = { // JSON data to send
    param1: 'value1',
    param2: 'value2',
    // ... other parameters
  };

  try {
    // Make the POST request to the server
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });

    // Check if the response is ok (status in the range 200-299)
    if (!response.ok) throw new Error('Network response was not ok.');

    // Get the blob data
    const blob = await response.blob();
    
    // Create a URL for the blob
    const blobUrl = URL.createObjectURL(blob);

    // Open the download in a new tab
    const newTab = window.open(blobUrl, '_blank');
    if (newTab) {
      newTab.focus();
    } else {
      alert('Please allow popups for this website');
    }

    // Release the URL after some time to free up resources
    setTimeout(() => URL.revokeObjectURL(blobUrl), 10000);
  } catch (error) {
    console.error('Error downloading report:', error);
  }
}
