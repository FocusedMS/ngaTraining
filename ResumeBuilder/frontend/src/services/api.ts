export const API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5189";

const authHeader = () => {
  const t = localStorage.getItem("token");
  return t ? { Authorization: `Bearer ${t}` } : {};
};

export async function http<T = any>(path: string, init?: RequestInit): Promise<T> {
  const res = await fetch(`${API_URL}${path}`, {
    ...init,
    headers: { "Content-Type": "application/json", ...(init?.headers || {}), ...authHeader() }
  });
  if (!res.ok) throw new Error(await res.text());
  const ct = res.headers.get("Content-Type") || "";
  if (ct.includes("application/json")) return res.json() as Promise<T>;
  // @ts-ignore
  return res;
}