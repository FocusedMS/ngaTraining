import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { api } from '../lib/api'
import toast from 'react-hot-toast'

type PendingPost = {
  id: number
  title: string
  slug: string
  authorId: number
  updatedAt: string
}
type PendingResp = {
  items: PendingPost[]
  total: number
  page: number
  pageSize: number
}

/** Small confirm toast (Yes/No) that resolves to boolean */
function confirmToast(message: string): Promise<boolean> {
  return new Promise((resolve) => {
    const id = toast.custom((t) => (
      <div className="card p-4 shadow-lg min-w-[280px]">
        <div className="font-medium mb-3">{message}</div>
        <div className="flex gap-2 justify-end">
          <button
            className="btn btn-outline"
            onClick={() => { toast.dismiss(t.id); resolve(false) }}
          >
            Cancel
          </button>
          <button
            className="btn btn-primary"
            onClick={() => { toast.dismiss(t.id); resolve(true) }}
          >
            Yes
          </button>
        </div>
      </div>
    ), { duration: Infinity, id: `confirm-${Date.now()}` })
  })
}

/** Prompt for a text (Reject reason). Returns string or null if canceled. */
function promptToast(label: string): Promise<string | null> {
  return new Promise((resolve) => {
    let value = ''
    toast.custom((t) => (
      <div className="card p-4 shadow-lg min-w-[320px]">
        <div className="font-medium mb-2">{label}</div>
        <input
          className="input mb-3 w-full"
          placeholder="Optional"
          onChange={(e) => { value = e.currentTarget.value }}
          autoFocus
        />
        <div className="flex gap-2 justify-end">
          <button
            className="btn btn-outline"
            onClick={() => { toast.dismiss(t.id); resolve(null) }}
          >
            Cancel
          </button>
          <button
            className="btn btn-primary"
            onClick={() => { toast.dismiss(t.id); resolve(value) }}
          >
            Submit
          </button>
        </div>
      </div>
    ), { duration: Infinity })
  })
}

export default function AdminModeration() {
  const qc = useQueryClient()

  const pendingQ = useQuery({
    queryKey: ['moderation', 'pending'],
    queryFn: async () => (await api.get<PendingResp>('/api/Moderation/posts')).data,
  })

  const approve = useMutation({
    mutationFn: async (id: number) =>
      (await api.post(`/api/Moderation/posts/${id}/approve`)).data,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['moderation', 'pending'] }),
  })

  const reject = useMutation({
    mutationFn: async (args: { id: number; reason: string }) =>
      (await api.post(`/api/Moderation/posts/${args.id}/reject`, { reason: args.reason })).data,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['moderation', 'pending'] }),
  })

  const handleApprove = async (p: PendingPost) => {
    const ok = await confirmToast(`Approve “${p.title}”?`)
    if (!ok) return
    await toast.promise(approve.mutateAsync(p.id), {
      loading: 'Approving…',
      success: 'Approved ✅',
      error: 'Approve failed',
    })
  }

  const handleReject = async (p: PendingPost) => {
    const ok = await confirmToast(`Reject “${p.title}”?`)
    if (!ok) return
    const reason = await promptToast('Rejection reason (optional)')
    await toast.promise(reject.mutateAsync({ id: p.id, reason: reason ?? '' }), {
      loading: 'Rejecting…',
      success: reason ? `Rejected (reason noted) ❌` : 'Rejected ❌',
      error: 'Reject failed',
    })
  }

  if (pendingQ.isLoading) return <div className="container py-8">Loading…</div>
  if (pendingQ.isError) return <div className="container py-8 text-red-600">Failed to load queue.</div>

  const rows = pendingQ.data?.items ?? []

  return (
    <div className="space-y-4">
      <h1 className="text-xl font-semibold">Moderation Queue</h1>

      {rows.length === 0 ? (
        <div className="card p-6 text-gray-600">No posts waiting for review.</div>
      ) : (
        <div className="overflow-x-auto">
          <table className="min-w-full text-sm">
            <thead>
              <tr className="border-b text-left">
                <th className="px-4 py-2">Title</th>
                <th className="px-4 py-2">Author</th>
                <th className="px-4 py-2">Updated</th>
                <th className="px-4 py-2">Actions</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((p) => (
                <tr key={p.id} className="border-b">
                  <td className="px-4 py-2">{p.title}</td>
                  <td className="px-4 py-2">#{p.authorId}</td>
                  <td className="px-4 py-2">{new Date(p.updatedAt).toLocaleString()}</td>
                  <td className="px-4 py-2 flex flex-wrap gap-2">
                    <button
                      className="btn btn-primary"
                      onClick={() => handleApprove(p)}
                      disabled={approve.isPending || reject.isPending}
                    >
                      {approve.isPending ? 'Approving…' : 'Approve'}
                    </button>
                    <button
                      className="btn btn-outline"
                      onClick={() => handleReject(p)}
                      disabled={approve.isPending || reject.isPending}
                    >
                      {reject.isPending ? 'Rejecting…' : 'Reject'}
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
