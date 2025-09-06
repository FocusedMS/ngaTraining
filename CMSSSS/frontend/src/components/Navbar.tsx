import { Link, useNavigate } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import type { RootState } from "../store";
import { logout } from "../store/slices/authSlice";

export default function Navbar() {
  const user = useSelector((s: RootState) => s.auth.user);
  const dispatch = useDispatch();
  const nav = useNavigate();

  const onLogout = () => {
    dispatch(logout());
    nav("/login");
  };

  return (
    <header className="sticky top-0 z-30 border-b bg-white/80 backdrop-blur">
      <div className="container py-3 flex items-center gap-4">
        <Link to="/" className="text-xl font-semibold">
          📝 Blog CMS
        </Link>

        <nav className="flex items-center gap-3">
          <Link to="/" className="btn btn-outline">Home</Link>
          <Link to="/editor" className="btn btn-outline">New Post</Link>
          <Link to="/dashboard" className="btn btn-outline">My Posts</Link>
          <Link to="/moderation" className="btn btn-outline">Moderation</Link>
        </nav>

        <div className="ml-auto flex items-center gap-3">
          {!user ? (
            <Link to="/login" className="btn btn-primary">Login</Link>
          ) : (
            <div className="flex items-center gap-2">
              <span className="text-sm px-2 py-1 rounded bg-gray-100">
                {user.username}
              </span>
              <span className="text-xs px-2 py-1 rounded bg-brand-100 text-brand-700">
                {user.role}
              </span>
              <button className="btn btn-outline" onClick={onLogout}>
                Logout
              </button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
}
