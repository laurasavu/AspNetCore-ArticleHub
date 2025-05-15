document.addEventListener("DOMContentLoaded", async () => {
    const results = await fetchData();
    if (results) {
        displayArticleTitles(results);
    }

    document.querySelector(".grid").addEventListener("click", async (event) => {
        if (event.target && event.target.id === "deleteButton") {
            const articleId = event.target.getAttribute("data-article-id");
            console.log("Delete button clicked for article ID:", articleId);
            await deleteArticle(articleId);
        }
    });

    const searchBar = document.querySelector(".search-bar");
    searchBar.addEventListener("input", (e) => {
        const searchTerm = e.target.value.toLowerCase();

        const filteredArticles = results.filter(article =>
            article.title.toLowerCase().includes(searchTerm)
        );

        const grid = document.querySelector(".grid");
        if (grid) {
            grid.innerHTML = ""; // Clear the grid
            displayArticleTitles(filteredArticles); // Re-render filtered articles
        }
    });
});

const fetchData = async () => {
    try {
        const response = await fetch("http://localhost:5096/api/Article/TitlesAndWriters");
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const results = await response.json();
        return results;
    } catch (error) {
        console.error("Error fetching article titles:", error);
        return null;
    }
};

const displayArticleTitles = (results) => {
    if (!Array.isArray(results)) {
        console.error("Data received is not an array:", results);
        return;
    }

    const container = document.querySelector(".container");
    const grid = container?.querySelector(".grid");

    if (!grid) {
        console.error("Grid container not found in DOM.");
        return;
    }

    grid.innerHTML = ""; 
    results.forEach(articleandauthor => {
        const articleId = articleandauthor.articleId;
        const articleTitle = articleandauthor.title;
        const writerName = articleandauthor.writerName;
        const writerid = articleandauthor.writerId;

        const item = document.createElement("div");
        item.classList.add("item");

        item.innerHTML = `
            <a href="viewArticle.html?articleId=${articleId}&writerName=${encodeURIComponent(writerName)}" class="article-title" data-article-id="${articleId}">
                <h3>${articleTitle}</h3>
            </a>
            <p>Written by: ${writerName}</p>
            <button onclick="window.location.href = 'EditArticle.html?articleId=${articleId}&writerid=${writerid}'">Edit Article</button>
            <button id="deleteButton" data-article-id="${articleId}">Delete Article</button>
        `;

        grid.appendChild(item);
    });
};

const confirmDelete = () => {
    return new Promise((resolve) => {
        const userConfirmed = window.confirm("Are you sure you want to delete this article?");
        resolve(userConfirmed);
    });
};

const deleteArticle = async (articleId) => {
    try {
        const userConfirmed = await confirmDelete();
        if (!userConfirmed) {
            console.log("Delete action canceled by the user.");
            return;
        }

        console.log("id" + articleId);
        const response = await fetch(`http://localhost:5096/api/Article/${articleId}`, {
            method: "DELETE"
        });
        if (response.status === 401) {
            alert("You do not have permission to delete this article.");
            return;
        }
        if (!response.ok) {
            throw new Error(`Failed to delete article. HTTP status: ${response.status}`);
        }

        console.log(`Article with ID ${articleId} deleted successfully.`);
        const articleElement = document.querySelector(`.item button[data-article-id="${articleId}"]`).parentElement;
        if (articleElement) {
            articleElement.remove();
        }
    } catch (error) {
        console.error("Error deleting article:", error);
    }
};
