function makeRow(book, isFavorite, onHeartClick) {
  const card = document.createElement("li");
  card.className = "card";

  const frame = document.createElement("div");
  frame.className = "frame";

  const heart = document.createElement("button");
  heart.className = isFavorite ? "heart on" : "heart";
  heart.textContent = isFavorite ? "♥" : "♡";
  heart.title = isFavorite ? "Remove from favorites" : "Add to favorites";
  heart.onclick = onHeartClick;

  frame.append(makeCover(book), heart);

  const info = document.createElement("div");
  info.className = "info";

  const title = document.createElement("div");
  title.className = "title";
  title.textContent = book.title;

  const meta = document.createElement("div");
  meta.className = "meta";
  const year = book.year ? book.year : "year unknown";
  meta.textContent = book.author + " · " + year;

  const badge = document.createElement("span");
  badge.className = "badge " + book.era;
  badge.textContent = book.era;

  info.append(title, meta, badge);

  card.append(frame, info);
  return card;
}

function makeCover(book) {
  if (!book.coverUrl) {
    return makePlaceholder(book, "Open Library did not send a cover number for this book.");
  }

  return makePicture(book, 1);
}

function makePicture(book, attempt) {
  const picture = document.createElement("img");
  picture.className = "cover";
  picture.src = book.coverUrl;
  picture.alt = "Cover of " + book.title;
  picture.loading = "lazy";

  picture.onerror = () => {
    console.warn("Cover failed to load (try " + attempt + "):", book.coverUrl);

    if (attempt === 1) {
      setTimeout(() => picture.replaceWith(makePicture(book, 2)), 2000);
      return;
    }

    picture.replaceWith(makePlaceholder(book, "The cover picture did not load."));
  };

  return picture;
}

function makePlaceholder(book, reason) {
  const box = document.createElement("div");
  box.className = "cover placeholder";

  box.setAttribute("role", "img");
  box.setAttribute("aria-label", "Image not available for " + book.title);
  box.title = reason;

  box.innerHTML =
    '<svg viewBox="0 0 64 64" width="64" height="64" fill="none" stroke="currentColor" ' +
    'stroke-width="3" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">' +
    '<path d="M10 12h18a6 6 0 0 1 4 2 6 6 0 0 1 4-2h18v40H36a6 6 0 0 0-4 2 6 6 0 0 0-4-2H10z"/>' +
    '<path d="M32 14v40"/></svg>' +
    '<span>Image not available</span>';

  return box;
}