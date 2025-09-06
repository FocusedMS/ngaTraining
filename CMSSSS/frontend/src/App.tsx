import { Routes, Route } from 'react-router-dom'
import Navbar from './components/Navbar'
import Home from './pages/Home'
import PostDetail from './pages/PostDetail'
import Login from './pages/Login'
import Dashboard from './pages/Dashboard'
import Editor from './pages/Editor'
import Moderation from './pages/Moderation'
import ProtectedRoute from './components/ProtectedRoute'
import AdminRoute from './components/AdminRoute'

export default function App() {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      <main className="container py-6 flex-1">
        <Routes>
          <Route path="/" element={<Home/>} />
          <Route path="/post/:slug" element={<PostDetail/>} />
          <Route path="/login" element={<Login/>} />
          {/* create */}
          <Route path="/editor" element={<ProtectedRoute><Editor/></ProtectedRoute>} />
          {/* edit (id in path) */}
          <Route path="/editor/:id" element={<ProtectedRoute><Editor/></ProtectedRoute>} />
          <Route path="/dashboard" element={<ProtectedRoute><Dashboard/></ProtectedRoute>} />
          <Route path="/moderation" element={<AdminRoute><Moderation/></AdminRoute>} />
        </Routes>
      </main>
      <footer className="border-t bg-white">
        <div className="container py-6 text-sm text-gray-500">© {new Date().getFullYear()} Blog CMS</div>
      </footer>
    </div>
  )
}
