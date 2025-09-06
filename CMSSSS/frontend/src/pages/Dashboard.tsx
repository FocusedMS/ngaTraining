import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { Link } from 'react-router-dom'
import { api } from '../lib/api'
import toast from 'react-hot-toast'

type Post = {
  id: number
  title: string
  slug: string
  excerpt?: string | null
  status: 'Draft' | 'PendingReview' | 'Published' | string
  updatedAt: string
  createdAt: string
  publishedAt?: string | null
}

function fmt(dt?: string | null) {
  if (!dt) return '—'
  const d = new Date(dt)
  return d.toLocaleString()
}

/** Promise-based confirm UI using react-hot-toast */
function confirmToast(opts: {
  title?: string
  message?: string
  confirmText?: string
  cancelText?: string
} = {}) {
  const {
    title = 'Delete this post?',
    message = 'This action cannot be undone.',
    confirmText = 'Delete',
    cancelText = 'Cancel',
  } = opts

  return new Promise<boolean>((resolve) => {
    const id = toast.custom(
      (t) => (
        <div
          role="alertdialog"
          aria-modal="true"
          aria-labelledby="confirm-title"
          className={`card p-4 shadow-lg max-w-sm w-[360px] bg-white transition-all duration-200 
            ${t.visible ? 'opacity-100 translate-y-0' : 'opacity-0 -translate-y-2'}`}
        >
          <div id="confirm-title" className="font-semibold mb-1">
            {title}
          </div>
          <div className="text-sm text-gray-600 mb-3">{message}</div>
          <div className="flex justify-end gap-2">
            <button
              className="btn btn-outline"
              onClick={() => {
                toast.dismiss(id)
                resolve(false)
              }}
            >
              {cancelText}
            </button>
            <button
              className="btn btn-danger"
              onClick={() => {
                toast.dismiss(id)
                resolve(true)
              }}
            >
              {confirmText}
            </button>
          </div>
        </div>
      ),
      { duration: Infinity }
    )
  })
}

export default function Dashboard() {
  const qc = useQueryClient()

  const postsQ = useQuery({
    queryKey: ['my-posts'],
    queryFn: async () => (await api.get<Post[]>('/api/Posts/me')).data,
  })

  const submit = useMutation({
    mutationFn: async (id: number) => (await api.post(`/api/Posts/${id}/submit`)).data,
  })

  const remove = useMutation({
    mutationFn: async (id: number) => (await api.delete(`/api/Posts/${id}`)).data,
  })

  const handleSubmit = async (id: number) => {
    try {
      await submit.mutateAsync(id)
      toast.success('Submitted for review')
      qc.invalidateQueries({ queryKey: ['my-posts'] })
    } catch (e: any) {
      toast.error(e?.response?.data ?? 'Submit failed')
    }
  }

  const handleDelete = async (id: number) => {
    const ok = await confirmToast({
      title: 'Delete this post?',
      message: 'This will permanently remove the draft.',
    })
    if (!ok) return

    try {
      await remove.mutateAsync(id)
      toast.success('Deleted')
      qc.invalidateQueries({ queryKey: ['my-posts'] })
    } catch {
      toast.error('Delete failed')
    }
  }

  if (postsQ.isLoading) return <div className="container py-8">Loading…</div>
  if (postsQ.isError) return <div className="container py-8 text-red-600">Failed to load posts.</div>

  const posts = postsQ.data || []

  return (
    <div className="space-y-4">
      <div className="flex items-center gap-3">
        <h1 className="text-xl font-semibold">My Posts</h1>
        <Link to="/editor" className="btn btn-primary ml-auto">
          New Post
        </Link>
      </div>

      {posts.length === 0 ? (
        <div className="card p-6 text-gray-600">No posts yet. Click “New Post”.</div>
      ) : (
        <div className="overflow-x-auto">
          <table className="min-w-full text-sm">
            <thead>
              <tr className="border-b text-left">
                <th className="px-4 py-2">Title</th>
                <th className="px-4 py-2">Status</th>
                <th className="px-4 py-2">Updated</th>
                <th className="px-4 py-2">Actions</th>
              </tr>
            </thead>
            <tbody>
              {posts.map((p) => (
                <tr key={p.id} className="border-b">
                  <td className="px-4 py-2">
                    <div className="font-medium">{p.title}</div>
                    <div className="text-gray-500">{p.excerpt || '—'}</div>
                  </td>
                  <td className="px-4 py-2">
                    <span
                      className={
                        'px-2 py-1 rounded text-xs ' +
                        (p.status === 'Published'
                          ? 'bg-green-100 text-green-700'
                          : p.status === 'PendingReview'
                          ? 'bg-amber-100 text-amber-700'
                          : 'bg-gray-100 text-gray-700')
                      }
                    >
                      {p.status}
                    </span>
                  </td>
                  <td className="px-4 py-2">{fmt(p.updatedAt)}</td>
                  <td className="px-4 py-2 flex flex-wrap gap-2">
                    {p.status === 'Published' ? (
                      <Link
                        to={`/post/${p.slug}`}
                        className="btn btn-outline"
                        target="_blank"
                        rel="noreferrer"
                      >
                        View
                      </Link>
                    ) : null}

                    {/* EDIT NOW GOES TO /editor/:id */}
                    <Link to={`/editor/${p.id}`} className="btn btn-outline">
                      Edit
                    </Link>

                    {p.status === 'Draft' && (
                      <button
                        className="btn btn-primary"
                        onClick={() => handleSubmit(p.id)}
                        disabled={submit.isPending}
                      >
                        {submit.isPending ? 'Submitting…' : 'Submit for review'}
                      </button>
                    )}

                    <button
                      className="btn btn-outline"
                      onClick={() => handleDelete(p.id)}
                      disabled={remove.isPending}
                    >
                      {remove.isPending ? 'Deleting…' : 'Delete'}
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  )
}
