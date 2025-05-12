document.addEventListener("DOMContentLoaded", async () => {
    const urlParams = new URLSearchParams(window.location.search);
    const articleId = urlParams.get("articleId");
    const writerId = urlParams.get("writerid"); // Fixed variable name to match usage

    if (!articleId || !writerId) { // Fixed variable name to match usage
        console.error("Article ID or Writer ID is missing in the URL.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5096/api/Article/${articleId}`);
        if (!response.ok) {
            throw new Error(`Failed to fetch article. HTTP status: ${response.status}`);
        }
        const article = await response.json();
        if (article) {
            populateForm(article);
        }
    } catch (error) {
        console.error("Error fetching article:", error); // Added error handling for fetch
    }

    const form = document.querySelector("#editArticleForm");
    if (form) { // Added null check for form
        form.addEventListener("submit", async (event) => {
            event.preventDefault();
            await updateArticle(articleId, writerId); // Pass writerId to the update function
        });
    } else {
        console.error("Edit article form not found."); // Added error handling for missing form
    }
});

const populateForm = (article) => {
    const titleInput = document.querySelector("#title");
    const contentInput = document.querySelector("#content");

    if (titleInput) titleInput.value = article.title || "";
    if (contentInput) contentInput.value = article.content || "";
};

const updateArticle = async (articleId, writerId) => {
    const titleInput = document.querySelector("#title");
    const contentInput = document.querySelector("#content");

    if (!titleInput || !contentInput) { // Added null checks for inputs
        console.error("Title or content input not found.");
        return;
    }

    const updatedArticle = {
        id: articleId,
        title: titleInput.value,
        content: contentInput.value,
        writerId: writerId,
        comments: [] // Added an empty comments array
    };

    try {
        const response = await fetch(`http://localhost:5096/api/Article/${articleId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(updatedArticle)
        });

        if (!response.ok) {
            throw new Error(`Failed to update article. HTTP status: ${response.status}`);
        }

        console.log("Article updated successfully.");
        window.location.href = "index.html";
    } catch (error) {
        console.error("Error updating article:", error);
    }
};
