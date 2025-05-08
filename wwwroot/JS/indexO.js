    document.addEventListener("DOMContentLoaded", () => {
        const container = document.getElementById("content");

        // Initialize the app
        initializeApp(container);
    });

    

// Update fetchData to handle errors more gracefully and log specific issues
const fetchData = async () => {
    const endpoints = ["api/Writer", "api/Article", "api/Comment"];
    const requests = endpoints.map(async (endpoint) => {
        try {
            const response = await fetch(`http://localhost:7075/${endpoint}`);
            if (!response.ok) {
                throw new Error(`Failed to fetch ${endpoint}: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error(`Error fetching ${endpoint}:`, error);
            return null; // Return null for failed requests
        }
    });

    const results = await Promise.all(requests);

    // Check if any of the results are null and throw an error if necessary
    if (results.some((result) => result === null)) {
        throw new Error("One or more endpoints failed to fetch data.");
    }

    return results;
};
    // Initialize the app by fetching and rendering data
    const initializeApp = async (container) => {
        try {
            const [writers, articles, comments] = await fetchData();
            renderWriters(container, writers, articles, comments);
        } catch (error) {
            console.error("Error initializing app:", error);
        }
    };

    // Render all writers and their associated articles and comments
    const renderWriters = (container, writers, articles, comments) => {
        container.innerHTML = writers
            .map((writers) => createWritersHTML(writers, articles, comments))
            .join("");
    };

    // Create HTML for a single writer
    const createWritersHTML = (writer, articles, comments) => {
        const writerArticles = articles.filter(
            (article) => article.writer.id === writer.id
        );
        const articlesHTML = writerArticles
            .map((article) => createArticleHTML(article, comments))
            .join("");

        return `
        <div class="writer">
          <h2>${writer.name}</h2>
          <p>Email: ${writer.email}</p>
          <button onclick="showArticleForm('${writer.id}')">Add Article</button>
          <div id="form-container-${writer.id}" class="form-container"></div>
          <div class="articles">
            <h3>Articles:</h3>
            ${articlesHTML || "<p>No articles yet.</p>"}
          </div>
        </div>
      `;
    };

    // Create HTML for a single article
    const createArticleHTML = (article, comments) => {
        const articleComments = comments.filter(
            (comment) => comment.articleId === article.id
        );
        const commentsHTML = articleComments
            .map((comment) => `<div class="comment"><p>${comment.content}</p></div>`)
            .join("");

        const writerName = article.writer ? article.writer.name : "Unknown Writer";
        const writerEmail = article.writer ? article.writer.email : "N/A";

        return `
        <div class="article">
          <h4>${article.title}</h4>
          <p>${article.content}</p>
          <p>Written by: ${writerName} (${writerEmail})</p>
          <button onclick="editArticle('${article.id}', '${article.writer.id}')">Edit Article</button>
          <button onclick="deleteArticle('${article.id}')">Delete Article</button>
          <div class="comments">
            <h5>Comments:</h5>
            ${commentsHTML || "<p>No comments yet.</p>"}
          </div>
        </div>
      `;
    };

    // Show the form for adding a new article
    const showArticleForm = (writerId) => {
        const formContainer = document.getElementById(`form-container-${writerId}`);
        if (formContainer.innerHTML) {
            formContainer.innerHTML = ""; // Hide the form if it's already visible
            return;
        }

        formContainer.innerHTML = `
        <form id="article-form-${writerId}" class="article-form">
          <h4>Add a New Article</h4>
          <label for="title-${writerId}">Title:</label>
          <input type="text" id="title-${writerId}" name="title" required />
          <label for="content-${writerId}">Content:</label>
          <textarea id="content-${writerId}" name="content" required></textarea>
          <button type="submit">Submit</button>
          <button type="button" onclick="hideArticleForm('${writerId}')">Cancel</button>
        </form>
      `;

        const form = document.getElementById(`article-form-${writerId}`);
        form.addEventListener("submit", (event) => {
            event.preventDefault();
            const title = document.getElementById(`title-${writerId}`).value;
            const content = document.getElementById(`content-${writerId}`).value;
            addArticle(writerId, title, content);
        });
    };

    // Hide the article form
    const hideArticleForm = (writerId) => {
        const formContainer = document.getElementById(`form-container-${writerId}`);
        formContainer.innerHTML = "";
    };

    // Add a new article for a writer
    const addArticle = async (writerId, title, content) => {
        const newArticle = {
            title,
            content,
            writer: {
                id: writerId, // Send the writer ID as part of the Writer object
            },
        };

        try {
            const response = await fetch("http://localhost:7075/api/Article", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newArticle),
            });
            const article = await response.json();
            alert(`Article "${article.title}" added successfully!`);
            location.reload(); // Reload the page to show the new article
        } catch (error) {
            console.error("Error adding article:", error);
        }
    };

    // Edit an existing article
    const editArticle = async (articleId, writerId) => {
        const updatedArticle = {
            id: articleId,
            title: document.getElementById(`edit-title-${articleId}`).value,
            content: document.getElementById(`edit-content-${articleId}`).value,
            writer: {
                id: writerId, // Send the writer ID as part of the Writer object
            },
        };

        try {
            const response = await fetch(
                `http://localhost:7075/api/Article/${articleId}`,
                {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(updatedArticle),
                }
            );
            const article = await response.json();
            alert(`Article "${article.title}" updated successfully!`);
            location.reload(); // Reload the page to show the updated article
        } catch (error) {
            console.error("Error updating article:", error);
        }
    };

    // Delete an article
    const deleteArticle = async (articleId) => {
        try {
            const response = await fetch(`http://localhost:7075/Article/${articleId}`, {
                method: "DELETE",
            });

            if (response.ok) {
                alert("Article deleted successfully!");
                location.reload(); // Reload the page to reflect the deletion
            } else {
                throw new Error("Failed to delete the article");
            }
        } catch (error) {
            console.error("Error deleting article:", error);
        }

    };