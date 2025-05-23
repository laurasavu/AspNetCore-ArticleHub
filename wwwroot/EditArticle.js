document.addEventListener("DOMContentLoaded", async () => {
    const urlParams = new URLSearchParams(window.location.search);
    const articleId = urlParams.get("articleId");
    const writerId = urlParams.get("writerid"); 

    if (!articleId || !writerId) {
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
        console.error("Error fetching article:", error);
    }

    const form = document.querySelector("#editArticleForm");
    if (form) { 
        form.addEventListener("submit", async (event) => {
            event.preventDefault();
            await updateArticle(articleId, writerId); 
        });
    } else {
        console.error("Edit article form not found."); 
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

    if (!titleInput || !contentInput) {
        console.error("Title or content input not found.");
        return;
    }

    const updatedArticle = {
        id: articleId,
        title: titleInput.value,
        content: contentInput.value,
        writerId: writerId,
        comments: [] 
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
