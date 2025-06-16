# Controllers

## 🔐 `LogInController`
Handles user authentication:
- `POST /api/LogIn/SignIn` – User sign-in
- `POST /api/LogIn/Refresh` – Refresh JWT token
- `POST /api/LogIn/Logout` – Revoke refresh token

---

## 📬 `ContactController`
Manages email-based contact actions:
- `POST /api/Contact/counselor` – Contact a specific counselor (authorized)
- `POST /api/Contact/general` – General contact to site manager (authorized)

---

## 🧑‍🏫 `CounselorController`
Handles counselor registration and management:
- `GET /api/Counselor/{id}` – Get counselor by ID
- `GET /api/Counselor` – Get all counselors
- `POST /api/Counselor/SignUp` – Register new counselor
- `PUT /api/Counselor/{id}` – Update counselor (authorized)
- `DELETE /api/Counselor/{id}` – Delete counselor (authorized)

---

## 📝 `PostController`
Handles posts in the forum:
- `GET /api/Post/{id}` – Get post by ID
- `GET /api/Post` – Get all posts
- `POST /api/Post` – Create new post (authorized)
- `PUT /api/Post/{id}` – Update post (authorized)
- `POST /api/Post/like/{id}` – Like a post
- `POST /api/Post/send-report-email/{postId}` – Send report email with delete link (authorized)
- `GET /api/Post/delete-post/{token}` – Get post content by delete token
- `DELETE /api/Post/delete-post/{token}` – Delete post using token

---

## 📚 `TopicController`
Handles forum topics:
- `GET /api/Topic/{id}` – Get topic by ID
- `GET /api/Topic` – Get all topics
- `POST /api/Topic` – Create topic (authorized)
- `PUT /api/Topic/{id}` – Update topic (authorized)
- `DELETE /api/Topic/{id}` – Delete topic (authorized)
- `GET /api/Topic/{id}/posts` – Get posts under a topic
