document.addEventListener("DOMContentLoaded", () => {
  const container = document.getElementById("content");

  // Initialize the app
  initializeApp(container);
});

// Fetch data from the server
const fetchData = async () => {
  const endpoints = ["api/writer", "api/article", "api/comment"];
  const requests = endpoints.map((endpoint) =>
      fetch(` https://localhost:7075}`).then((res) => res.json())
  );
  return Promise.all(requests);
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