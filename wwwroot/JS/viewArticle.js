(async function () {
    // Extract articleId and writerName from the URL
    const urlParams = new URLSearchParams(window.location.search);
    const articleId = urlParams.get("articleId");
    const writerName = urlParams.get("writerName");

    // Ensure articleId is defined before proceeding
    if (typeof articleId !== "undefined" && articleId) {
        try {
            // Fetch article data from the database via an API
            const response = await fetch(`/api/Article/${articleId}`);

            if (!response.ok) {
                throw new Error("Failed to fetch article data.");
            }

            const articleData = await response.json();

            // Validate article data
            if (!articleData || !articleData.title || !articleData.content) {
                throw new Error("Invalid article data received.");
            }

            // Populate the HTML elements with the article data
            document.getElementById("articleTitle").textContent = articleData.title;
            document.getElementById("articleAuthor").textContent = writerName
                ? `By: ${decodeURIComponent(writerName)}`
                : "Author Unknown";
            document.getElementById("articleContent").textContent = articleData.content;

            // Fetch comments separately using articleId
            const commentsResponse = await fetch(`/api/Comment/Article/${articleId}/Comments/Content`);

            const commentsData = await commentsResponse.json();

            // Handle comments
            const commentsContainer = document.getElementById("articleComments");
            commentsContainer.innerHTML = ""; // Clear any existing comments
          
            if (commentsData && Array.isArray(commentsData)) {
                if (commentsData.length > 0) {
                    if (commentsData && Array.isArray(commentsData)) {
                        if (commentsData.length > 0) {
                            // Add the heading before listing comments
                            const heading = document.createElement("h2");
                            heading.textContent = "Comments on this Article";
                            commentsContainer.appendChild(heading);

                            commentsData.forEach((comment) => {
                                const commentElement = document.createElement("div");
                                commentElement.classList.add("comment");

                                const textElement = document.createElement("p");

                                textElement.textContent = comment;
                                commentElement.appendChild(textElement);
                                commentsContainer.appendChild(commentElement);
                            });
                        } else {
                            commentsContainer.innerHTML = "<p>No comments available.</p>";
                        }
                    } else {
                        commentsContainer.innerHTML = "<p>No comments available.</p>";
                    }
                } else {
                    commentsContainer.innerHTML = "<p>No comments available.</p>";
                }
            } else {
                commentsContainer.innerHTML = "<p>No comments available.</p>";
            }
        } catch (error) {
            // Handle errors during the fetch process
            document.getElementById("articleTitle").textContent = "Error Loading Article";
            document.getElementById("articleAuthor").textContent = "";
            document.getElementById("articleContent").innerHTML =
                `<p>There was an error loading the requested article. Please try again later.</p>`;
            document.getElementById("articleComments").innerHTML =
                `<p>Unable to load comments.</p>`;
            console.error(error);
        }
    } else {
        console.error("articleId is not defined. Ensure that the variable 'articleId' is declared and assigned a valid value before this script runs.");
    }
})();