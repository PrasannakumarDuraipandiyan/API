window.downloadFile = async (url, postData, filename = 'downloaded_file') => {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postData)
        });

        if (!response.ok) throw new Error("Failed to fetch the file.");

        const blob = await response.blob();
        const blobUrl = URL.createObjectURL(blob);

        // Create a temporary anchor element to trigger the download
        const a = document.createElement('a');
        a.href = blobUrl;
        a.download = filename;  // Set the filename for the downloaded file

        // Append to the body, click to start download, and remove it
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        // Clean up the blob URL after some time
        setTimeout(() => URL.revokeObjectURL(blobUrl), 10000);
    } catch (error) {
        console.error("Download failed:", error);
    }
};
