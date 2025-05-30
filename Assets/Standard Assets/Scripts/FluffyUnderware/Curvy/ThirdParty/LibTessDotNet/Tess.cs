// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ThirdParty.LibTessDotNet.Tess
using System;
using System.Runtime.CompilerServices;

namespace FluffyUnderware.Curvy.ThirdParty.LibTessDotNet
{
	public class Tess
	{
		public Tess()
		{
			this._normal = Vec3.Zero;
			this._bminX = (this._bminY = (this._bmaxX = (this._bmaxY = 0f)));
			this._windingRule = WindingRule.EvenOdd;
			this._mesh = null;
			this._vertices = null;
			this._vertexCount = 0;
			this._elements = null;
			this._elementCount = 0;
		}

		private Tess.ActiveRegion RegionBelow(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._prev._key;
		}

		private Tess.ActiveRegion RegionAbove(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._next._key;
		}

		private bool EdgeLeq(Tess.ActiveRegion reg1, Tess.ActiveRegion reg2)
		{
			MeshUtils.Edge eUp = reg1._eUp;
			MeshUtils.Edge eUp2 = reg2._eUp;
			if (eUp._Dst == this._event)
			{
				if (eUp2._Dst != this._event)
				{
					return Geom.EdgeSign(eUp2._Dst, this._event, eUp2._Org) <= 0f;
				}
				if (Geom.VertLeq(eUp._Org, eUp2._Org))
				{
					return Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) <= 0f;
				}
				return Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) >= 0f;
			}
			else
			{
				if (eUp2._Dst == this._event)
				{
					return Geom.EdgeSign(eUp._Dst, this._event, eUp._Org) >= 0f;
				}
				float num = Geom.EdgeEval(eUp._Dst, this._event, eUp._Org);
				float num2 = Geom.EdgeEval(eUp2._Dst, this._event, eUp2._Org);
				return num >= num2;
			}
		}

		private void DeleteRegion(Tess.ActiveRegion reg)
		{
			if (reg._fixUpperEdge)
			{
			}
			reg._eUp._activeRegion = null;
			this._dict.Remove(reg._nodeUp);
		}

		private void FixUpperEdge(Tess.ActiveRegion reg, MeshUtils.Edge newEdge)
		{
			this._mesh.Delete(reg._eUp);
			reg._fixUpperEdge = false;
			reg._eUp = newEdge;
			newEdge._activeRegion = reg;
		}

		private Tess.ActiveRegion TopLeftRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Vertex org = reg._eUp._Org;
			do
			{
				reg = this.RegionAbove(reg);
			}
			while (reg._eUp._Org == org);
			if (reg._fixUpperEdge)
			{
				MeshUtils.Edge newEdge = this._mesh.Connect(this.RegionBelow(reg)._eUp._Sym, reg._eUp._Lnext);
				this.FixUpperEdge(reg, newEdge);
				reg = this.RegionAbove(reg);
			}
			return reg;
		}

		private Tess.ActiveRegion TopRightRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Vertex dst = reg._eUp._Dst;
			do
			{
				reg = this.RegionAbove(reg);
			}
			while (reg._eUp._Dst == dst);
			return reg;
		}

		private Tess.ActiveRegion AddRegionBelow(Tess.ActiveRegion regAbove, MeshUtils.Edge eNewUp)
		{
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = eNewUp;
			activeRegion._nodeUp = this._dict.InsertBefore(regAbove._nodeUp, activeRegion);
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = false;
			activeRegion._dirty = false;
			eNewUp._activeRegion = activeRegion;
			return activeRegion;
		}

		private void ComputeWinding(Tess.ActiveRegion reg)
		{
			reg._windingNumber = this.RegionAbove(reg)._windingNumber + reg._eUp._winding;
			reg._inside = Geom.IsWindingInside(this._windingRule, reg._windingNumber);
		}

		private void FinishRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Edge eUp = reg._eUp;
			MeshUtils.Face lface = eUp._Lface;
			lface._inside = reg._inside;
			lface._anEdge = eUp;
			this.DeleteRegion(reg);
		}

		private MeshUtils.Edge FinishLeftRegions(Tess.ActiveRegion regFirst, Tess.ActiveRegion regLast)
		{
			Tess.ActiveRegion activeRegion = regFirst;
			MeshUtils.Edge eUp = regFirst._eUp;
			while (activeRegion != regLast)
			{
				activeRegion._fixUpperEdge = false;
				Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
				MeshUtils.Edge edge = activeRegion2._eUp;
				if (edge._Org != eUp._Org)
				{
					if (!activeRegion2._fixUpperEdge)
					{
						this.FinishRegion(activeRegion);
						break;
					}
					edge = this._mesh.Connect(eUp._Lprev, edge._Sym);
					this.FixUpperEdge(activeRegion2, edge);
				}
				if (eUp._Onext != edge)
				{
					this._mesh.Splice(edge._Oprev, edge);
					this._mesh.Splice(eUp, edge);
				}
				this.FinishRegion(activeRegion);
				eUp = activeRegion2._eUp;
				activeRegion = activeRegion2;
			}
			return eUp;
		}

		private void AddRightEdges(Tess.ActiveRegion regUp, MeshUtils.Edge eFirst, MeshUtils.Edge eLast, MeshUtils.Edge eTopLeft, bool cleanUp)
		{
			bool flag = true;
			MeshUtils.Edge edge = eFirst;
			do
			{
				this.AddRegionBelow(regUp, edge._Sym);
				edge = edge._Onext;
			}
			while (edge != eLast);
			if (eTopLeft == null)
			{
				eTopLeft = this.RegionBelow(regUp)._eUp._Rprev;
			}
			Tess.ActiveRegion activeRegion = regUp;
			MeshUtils.Edge edge2 = eTopLeft;
			for (;;)
			{
				Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
				edge = activeRegion2._eUp._Sym;
				if (edge._Org != edge2._Org)
				{
					break;
				}
				if (edge._Onext != edge2)
				{
					this._mesh.Splice(edge._Oprev, edge);
					this._mesh.Splice(edge2._Oprev, edge);
				}
				activeRegion2._windingNumber = activeRegion._windingNumber - edge._winding;
				activeRegion2._inside = Geom.IsWindingInside(this._windingRule, activeRegion2._windingNumber);
				activeRegion._dirty = true;
				if (!flag && this.CheckForRightSplice(activeRegion))
				{
					Geom.AddWinding(edge, edge2);
					this.DeleteRegion(activeRegion);
					this._mesh.Delete(edge2);
				}
				flag = false;
				activeRegion = activeRegion2;
				edge2 = edge;
			}
			activeRegion._dirty = true;
			if (cleanUp)
			{
				this.WalkDirtyRegions(activeRegion);
			}
		}

		private void SpliceMergeVertices(MeshUtils.Edge e1, MeshUtils.Edge e2)
		{
			this._mesh.Splice(e1, e2);
		}

		private void VertexWeights(MeshUtils.Vertex isect, MeshUtils.Vertex org, MeshUtils.Vertex dst, out float w0, out float w1)
		{
			float num = Geom.VertL1dist(org, isect);
			float num2 = Geom.VertL1dist(dst, isect);
			w0 = num2 / (num + num2) / 2f;
			w1 = num / (num + num2) / 2f;
			isect._coords.X = isect._coords.X + (w0 * org._coords.X + w1 * dst._coords.X);
			isect._coords.Y = isect._coords.Y + (w0 * org._coords.Y + w1 * dst._coords.Y);
			isect._coords.Z = isect._coords.Z + (w0 * org._coords.Z + w1 * dst._coords.Z);
		}

		private void GetIntersectData(MeshUtils.Vertex isect, MeshUtils.Vertex orgUp, MeshUtils.Vertex dstUp, MeshUtils.Vertex orgLo, MeshUtils.Vertex dstLo)
		{
			isect._coords = Vec3.Zero;
			float num;
			float num2;
			this.VertexWeights(isect, orgUp, dstUp, out num, out num2);
			float num3;
			float num4;
			this.VertexWeights(isect, orgLo, dstLo, out num3, out num4);
			if (this._combineCallback != null)
			{
				isect._data = this._combineCallback(isect._coords, new object[]
				{
					orgUp._data,
					dstUp._data,
					orgLo._data,
					dstLo._data
				}, new float[]
				{
					num,
					num2,
					num3,
					num4
				});
			}
		}

		private bool CheckForRightSplice(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (Geom.VertLeq(eUp._Org, eUp2._Org))
			{
				if (Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) > 0f)
				{
					return false;
				}
				if (!Geom.VertEq(eUp._Org, eUp2._Org))
				{
					this._mesh.SplitEdge(eUp2._Sym);
					this._mesh.Splice(eUp, eUp2._Oprev);
					regUp._dirty = (activeRegion._dirty = true);
				}
				else if (eUp._Org != eUp2._Org)
				{
					this._pq.Remove(eUp._Org._pqHandle);
					this.SpliceMergeVertices(eUp2._Oprev, eUp);
				}
			}
			else
			{
				if (Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) < 0f)
				{
					return false;
				}
				this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
				this._mesh.SplitEdge(eUp._Sym);
				this._mesh.Splice(eUp2._Oprev, eUp);
			}
			return true;
		}

		private bool CheckForLeftSplice(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (Geom.VertLeq(eUp._Dst, eUp2._Dst))
			{
				if (Geom.EdgeSign(eUp._Dst, eUp2._Dst, eUp._Org) < 0f)
				{
					return false;
				}
				this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
				MeshUtils.Edge edge = this._mesh.SplitEdge(eUp);
				this._mesh.Splice(eUp2._Sym, edge);
				edge._Lface._inside = regUp._inside;
			}
			else
			{
				if (Geom.EdgeSign(eUp2._Dst, eUp._Dst, eUp2._Org) > 0f)
				{
					return false;
				}
				regUp._dirty = (activeRegion._dirty = true);
				MeshUtils.Edge edge2 = this._mesh.SplitEdge(eUp2);
				this._mesh.Splice(eUp._Lnext, eUp2._Sym);
				edge2._Rface._inside = regUp._inside;
			}
			return true;
		}

		private bool CheckForIntersect(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge edge = activeRegion._eUp;
			MeshUtils.Vertex org = eUp._Org;
			MeshUtils.Vertex org2 = edge._Org;
			MeshUtils.Vertex dst = eUp._Dst;
			MeshUtils.Vertex dst2 = edge._Dst;
			if (org == org2)
			{
				return false;
			}
			float num = Math.Min(org._t, dst._t);
			float num2 = Math.Max(org2._t, dst2._t);
			if (num > num2)
			{
				return false;
			}
			if (Geom.VertLeq(org, org2))
			{
				if (Geom.EdgeSign(dst2, org, org2) > 0f)
				{
					return false;
				}
			}
			else if (Geom.EdgeSign(dst, org2, org) < 0f)
			{
				return false;
			}
			MeshUtils.Vertex vertex = MeshUtils.Pooled<MeshUtils.Vertex>.Create();
			Geom.EdgeIntersect(dst, org, dst2, org2, vertex);
			if (Geom.VertLeq(vertex, this._event))
			{
				vertex._s = this._event._s;
				vertex._t = this._event._t;
			}
			MeshUtils.Vertex vertex2 = (!Geom.VertLeq(org, org2)) ? org2 : org;
			if (Geom.VertLeq(vertex2, vertex))
			{
				vertex._s = vertex2._s;
				vertex._t = vertex2._t;
			}
			if (Geom.VertEq(vertex, org) || Geom.VertEq(vertex, org2))
			{
				this.CheckForRightSplice(regUp);
				return false;
			}
			if ((!Geom.VertEq(dst, this._event) && Geom.EdgeSign(dst, this._event, vertex) >= 0f) || (!Geom.VertEq(dst2, this._event) && Geom.EdgeSign(dst2, this._event, vertex) <= 0f))
			{
				if (dst2 == this._event)
				{
					this._mesh.SplitEdge(eUp._Sym);
					this._mesh.Splice(edge._Sym, eUp);
					regUp = this.TopLeftRegion(regUp);
					eUp = this.RegionBelow(regUp)._eUp;
					this.FinishLeftRegions(this.RegionBelow(regUp), activeRegion);
					this.AddRightEdges(regUp, eUp._Oprev, eUp, eUp, true);
					return true;
				}
				if (dst == this._event)
				{
					this._mesh.SplitEdge(edge._Sym);
					this._mesh.Splice(eUp._Lnext, edge._Oprev);
					activeRegion = regUp;
					regUp = this.TopRightRegion(regUp);
					MeshUtils.Edge rprev = this.RegionBelow(regUp)._eUp._Rprev;
					activeRegion._eUp = edge._Oprev;
					edge = this.FinishLeftRegions(activeRegion, null);
					this.AddRightEdges(regUp, edge._Onext, eUp._Rprev, rprev, true);
					return true;
				}
				if (Geom.EdgeSign(dst, this._event, vertex) >= 0f)
				{
					this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
					this._mesh.SplitEdge(eUp._Sym);
					eUp._Org._s = this._event._s;
					eUp._Org._t = this._event._t;
				}
				if (Geom.EdgeSign(dst2, this._event, vertex) <= 0f)
				{
					regUp._dirty = (activeRegion._dirty = true);
					this._mesh.SplitEdge(edge._Sym);
					edge._Org._s = this._event._s;
					edge._Org._t = this._event._t;
				}
				return false;
			}
			else
			{
				this._mesh.SplitEdge(eUp._Sym);
				this._mesh.SplitEdge(edge._Sym);
				this._mesh.Splice(edge._Oprev, eUp);
				eUp._Org._s = vertex._s;
				eUp._Org._t = vertex._t;
				eUp._Org._pqHandle = this._pq.Insert(eUp._Org);
				if (eUp._Org._pqHandle._handle == PQHandle.Invalid)
				{
					throw new InvalidOperationException("PQHandle should not be invalid");
				}
				this.GetIntersectData(eUp._Org, org, dst, org2, dst2);
				this.RegionAbove(regUp)._dirty = (regUp._dirty = (activeRegion._dirty = true));
				return false;
			}
		}

		private void WalkDirtyRegions(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			for (;;)
			{
				while (activeRegion._dirty)
				{
					regUp = activeRegion;
					activeRegion = this.RegionBelow(activeRegion);
				}
				if (!regUp._dirty)
				{
					activeRegion = regUp;
					regUp = this.RegionAbove(regUp);
					if (regUp == null || !regUp._dirty)
					{
						break;
					}
				}
				regUp._dirty = false;
				MeshUtils.Edge eUp = regUp._eUp;
				MeshUtils.Edge eUp2 = activeRegion._eUp;
				if (eUp._Dst != eUp2._Dst && this.CheckForLeftSplice(regUp))
				{
					if (activeRegion._fixUpperEdge)
					{
						this.DeleteRegion(activeRegion);
						this._mesh.Delete(eUp2);
						activeRegion = this.RegionBelow(regUp);
						eUp2 = activeRegion._eUp;
					}
					else if (regUp._fixUpperEdge)
					{
						this.DeleteRegion(regUp);
						this._mesh.Delete(eUp);
						regUp = this.RegionAbove(activeRegion);
						eUp = regUp._eUp;
					}
				}
				if (eUp._Org != eUp2._Org)
				{
					if (eUp._Dst != eUp2._Dst && !regUp._fixUpperEdge && !activeRegion._fixUpperEdge && (eUp._Dst == this._event || eUp2._Dst == this._event))
					{
						if (this.CheckForIntersect(regUp))
						{
							return;
						}
					}
					else
					{
						this.CheckForRightSplice(regUp);
					}
				}
				if (eUp._Org == eUp2._Org && eUp._Dst == eUp2._Dst)
				{
					Geom.AddWinding(eUp2, eUp);
					this.DeleteRegion(regUp);
					this._mesh.Delete(eUp);
					regUp = this.RegionAbove(activeRegion);
				}
			}
		}

		private void ConnectRightVertex(Tess.ActiveRegion regUp, MeshUtils.Edge eBottomLeft)
		{
			MeshUtils.Edge edge = eBottomLeft._Onext;
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			bool flag = false;
			if (eUp._Dst != eUp2._Dst)
			{
				this.CheckForIntersect(regUp);
			}
			if (Geom.VertEq(eUp._Org, this._event))
			{
				this._mesh.Splice(edge._Oprev, eUp);
				regUp = this.TopLeftRegion(regUp);
				edge = this.RegionBelow(regUp)._eUp;
				this.FinishLeftRegions(this.RegionBelow(regUp), activeRegion);
				flag = true;
			}
			if (Geom.VertEq(eUp2._Org, this._event))
			{
				this._mesh.Splice(eBottomLeft, eUp2._Oprev);
				eBottomLeft = this.FinishLeftRegions(activeRegion, null);
				flag = true;
			}
			if (flag)
			{
				this.AddRightEdges(regUp, eBottomLeft._Onext, edge, edge, true);
				return;
			}
			MeshUtils.Edge edge2;
			if (Geom.VertLeq(eUp2._Org, eUp._Org))
			{
				edge2 = eUp2._Oprev;
			}
			else
			{
				edge2 = eUp;
			}
			edge2 = this._mesh.Connect(eBottomLeft._Lprev, edge2);
			this.AddRightEdges(regUp, edge2, edge2._Onext, edge2._Onext, false);
			edge2._Sym._activeRegion._fixUpperEdge = true;
			this.WalkDirtyRegions(regUp);
		}

		private void ConnectLeftDegenerate(Tess.ActiveRegion regUp, MeshUtils.Vertex vEvent)
		{
			MeshUtils.Edge eUp = regUp._eUp;
			if (Geom.VertEq(eUp._Org, vEvent))
			{
				throw new InvalidOperationException("Vertices should have been merged before");
			}
			if (!Geom.VertEq(eUp._Dst, vEvent))
			{
				this._mesh.SplitEdge(eUp._Sym);
				if (regUp._fixUpperEdge)
				{
					this._mesh.Delete(eUp._Onext);
					regUp._fixUpperEdge = false;
				}
				this._mesh.Splice(vEvent._anEdge, eUp);
				this.SweepEvent(vEvent);
				return;
			}
			throw new InvalidOperationException("Vertices should have been merged before");
		}

		private void ConnectLeftVertex(MeshUtils.Vertex vEvent)
		{
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = vEvent._anEdge._Sym;
			Tess.ActiveRegion key = this._dict.Find(activeRegion).Key;
			Tess.ActiveRegion activeRegion2 = this.RegionBelow(key);
			if (activeRegion2 == null)
			{
				return;
			}
			MeshUtils.Edge eUp = key._eUp;
			MeshUtils.Edge eUp2 = activeRegion2._eUp;
			if (Geom.EdgeSign(eUp._Dst, vEvent, eUp._Org) == 0f)
			{
				this.ConnectLeftDegenerate(key, vEvent);
				return;
			}
			Tess.ActiveRegion activeRegion3 = (!Geom.VertLeq(eUp2._Dst, eUp._Dst)) ? activeRegion2 : key;
			if (key._inside || activeRegion3._fixUpperEdge)
			{
				MeshUtils.Edge edge;
				if (activeRegion3 == key)
				{
					edge = this._mesh.Connect(vEvent._anEdge._Sym, eUp._Lnext);
				}
				else
				{
					edge = this._mesh.Connect(eUp2._Dnext, vEvent._anEdge)._Sym;
				}
				if (activeRegion3._fixUpperEdge)
				{
					this.FixUpperEdge(activeRegion3, edge);
				}
				else
				{
					this.ComputeWinding(this.AddRegionBelow(key, edge));
				}
				this.SweepEvent(vEvent);
			}
			else
			{
				this.AddRightEdges(key, vEvent._anEdge, vEvent._anEdge, null, true);
			}
		}

		private void SweepEvent(MeshUtils.Vertex vEvent)
		{
			this._event = vEvent;
			MeshUtils.Edge edge = vEvent._anEdge;
			while (edge._activeRegion == null)
			{
				edge = edge._Onext;
				if (edge == vEvent._anEdge)
				{
					this.ConnectLeftVertex(vEvent);
					return;
				}
			}
			Tess.ActiveRegion activeRegion = this.TopLeftRegion(edge._activeRegion);
			Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
			MeshUtils.Edge eUp = activeRegion2._eUp;
			MeshUtils.Edge edge2 = this.FinishLeftRegions(activeRegion2, null);
			if (edge2._Onext == eUp)
			{
				this.ConnectRightVertex(activeRegion, edge2);
			}
			else
			{
				this.AddRightEdges(activeRegion, edge2._Onext, eUp, eUp, true);
			}
		}

		private void AddSentinel(float smin, float smax, float t)
		{
			MeshUtils.Edge edge = this._mesh.MakeEdge();
			edge._Org._s = smax;
			edge._Org._t = t;
			edge._Dst._s = smin;
			edge._Dst._t = t;
			this._event = edge._Dst;
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = edge;
			activeRegion._windingNumber = 0;
			activeRegion._inside = false;
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = true;
			activeRegion._dirty = false;
			activeRegion._nodeUp = this._dict.Insert(activeRegion);
		}

		private void InitEdgeDict()
		{
			this._dict = new Dict<Tess.ActiveRegion>(new Dict<Tess.ActiveRegion>.LessOrEqual(this.EdgeLeq));
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, -this.SentinelCoord);
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, this.SentinelCoord);
		}

		private void DoneEdgeDict()
		{
			Tess.ActiveRegion key;
			while ((key = this._dict.Min().Key) != null)
			{
				if (!key._sentinel)
				{
				}
				this.DeleteRegion(key);
			}
			this._dict = null;
		}

		private void RemoveDegenerateEdges()
		{
			MeshUtils.Edge eHead = this._mesh._eHead;
			MeshUtils.Edge next;
			for (MeshUtils.Edge edge = eHead._next; edge != eHead; edge = next)
			{
				next = edge._next;
				MeshUtils.Edge lnext = edge._Lnext;
				if (Geom.VertEq(edge._Org, edge._Dst) && edge._Lnext._Lnext != edge)
				{
					this.SpliceMergeVertices(lnext, edge);
					this._mesh.Delete(edge);
					edge = lnext;
					lnext = edge._Lnext;
				}
				if (lnext._Lnext == edge)
				{
					if (lnext != edge)
					{
						if (lnext == next || lnext == next._Sym)
						{
							next = next._next;
						}
						this._mesh.Delete(lnext);
					}
					if (edge == next || edge == next._Sym)
					{
						next = next._next;
					}
					this._mesh.Delete(edge);
				}
			}
		}

		private void InitPriorityQ()
		{
			MeshUtils.Vertex vHead = this._mesh._vHead;
			int num = 0;
			for (MeshUtils.Vertex next = vHead._next; next != vHead; next = next._next)
			{
				num++;
			}
			num += 8;
			int initialSize = num;
			if (Tess._003C_003Ef__mg_0024cache0 == null)
			{
				Tess._003C_003Ef__mg_0024cache0 = new PriorityHeap<MeshUtils.Vertex>.LessOrEqual(Geom.VertLeq);
			}
			this._pq = new PriorityQueue<MeshUtils.Vertex>(initialSize, Tess._003C_003Ef__mg_0024cache0);
			vHead = this._mesh._vHead;
			for (MeshUtils.Vertex next = vHead._next; next != vHead; next = next._next)
			{
				next._pqHandle = this._pq.Insert(next);
				if (next._pqHandle._handle == PQHandle.Invalid)
				{
					throw new InvalidOperationException("PQHandle should not be invalid");
				}
			}
			this._pq.Init();
		}

		private void DonePriorityQ()
		{
			this._pq = null;
		}

		private void RemoveDegenerateFaces()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				MeshUtils.Edge anEdge = face._anEdge;
				if (anEdge._Lnext._Lnext == anEdge)
				{
					Geom.AddWinding(anEdge._Onext, anEdge);
					this._mesh.Delete(anEdge);
				}
			}
		}

		protected void ComputeInterior()
		{
			this.RemoveDegenerateEdges();
			this.InitPriorityQ();
			this.RemoveDegenerateFaces();
			this.InitEdgeDict();
			MeshUtils.Vertex vertex;
			while ((vertex = this._pq.ExtractMin()) != null)
			{
				for (;;)
				{
					MeshUtils.Vertex vertex2 = this._pq.Minimum();
					if (vertex2 == null || !Geom.VertEq(vertex2, vertex))
					{
						break;
					}
					vertex2 = this._pq.ExtractMin();
					this.SpliceMergeVertices(vertex._anEdge, vertex2._anEdge);
				}
				this.SweepEvent(vertex);
			}
			this.DoneEdgeDict();
			this.DonePriorityQ();
			this.RemoveDegenerateFaces();
		}

		public Vec3 Normal
		{
			get
			{
				return this._normal;
			}
			set
			{
				this._normal = value;
			}
		}

		public ContourVertex[] Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		public int VertexCount
		{
			get
			{
				return this._vertexCount;
			}
		}

		public int[] Elements
		{
			get
			{
				return this._elements;
			}
		}

		public int ElementCount
		{
			get
			{
				return this._elementCount;
			}
		}

		private void ComputeNormal(ref Vec3 norm)
		{
			MeshUtils.Vertex next = this._mesh._vHead._next;
			float[] array = new float[]
			{
				next._coords.X,
				next._coords.Y,
				next._coords.Z
			};
			MeshUtils.Vertex[] array2 = new MeshUtils.Vertex[]
			{
				next,
				next,
				next
			};
			float[] array3 = new float[]
			{
				next._coords.X,
				next._coords.Y,
				next._coords.Z
			};
			MeshUtils.Vertex[] array4 = new MeshUtils.Vertex[]
			{
				next,
				next,
				next
			};
			while (next != this._mesh._vHead)
			{
				if (next._coords.X < array[0])
				{
					array[0] = next._coords.X;
					array2[0] = next;
				}
				if (next._coords.Y < array[1])
				{
					array[1] = next._coords.Y;
					array2[1] = next;
				}
				if (next._coords.Z < array[2])
				{
					array[2] = next._coords.Z;
					array2[2] = next;
				}
				if (next._coords.X > array3[0])
				{
					array3[0] = next._coords.X;
					array4[0] = next;
				}
				if (next._coords.Y > array3[1])
				{
					array3[1] = next._coords.Y;
					array4[1] = next;
				}
				if (next._coords.Z > array3[2])
				{
					array3[2] = next._coords.Z;
					array4[2] = next;
				}
				next = next._next;
			}
			int num = 0;
			if (array3[1] - array[1] > array3[0] - array[0])
			{
				num = 1;
			}
			if (array3[2] - array[2] > array3[num] - array[num])
			{
				num = 2;
			}
			if (array[num] >= array3[num])
			{
				norm = new Vec3
				{
					X = 0f,
					Y = 0f,
					Z = 1f
				};
				return;
			}
			float num2 = 0f;
			MeshUtils.Vertex vertex = array2[num];
			MeshUtils.Vertex vertex2 = array4[num];
			Vec3 vec;
			Vec3.Sub(ref vertex._coords, ref vertex2._coords, out vec);
			for (next = this._mesh._vHead._next; next != this._mesh._vHead; next = next._next)
			{
				Vec3 vec2;
				Vec3.Sub(ref next._coords, ref vertex2._coords, out vec2);
				Vec3 vec3;
				vec3.X = vec.Y * vec2.Z - vec.Z * vec2.Y;
				vec3.Y = vec.Z * vec2.X - vec.X * vec2.Z;
				vec3.Z = vec.X * vec2.Y - vec.Y * vec2.X;
				float num3 = vec3.X * vec3.X + vec3.Y * vec3.Y + vec3.Z * vec3.Z;
				if (num3 > num2)
				{
					num2 = num3;
					norm = vec3;
				}
			}
			if (num2 <= 0f)
			{
				norm = Vec3.Zero;
				num = Vec3.LongAxis(ref vec);
				norm[num] = 1f;
			}
		}

		private void CheckOrientation()
		{
			float num = 0f;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._anEdge._winding > 0)
				{
					num += MeshUtils.FaceArea(next);
				}
			}
			if (num < 0f)
			{
				for (MeshUtils.Vertex next2 = this._mesh._vHead._next; next2 != this._mesh._vHead; next2 = next2._next)
				{
					next2._t = -next2._t;
				}
				Vec3.Neg(ref this._tUnit);
			}
		}

		private void ProjectPolygon()
		{
			Vec3 normal = this._normal;
			bool flag = false;
			if (normal.X == 0f && normal.Y == 0f && normal.Z == 0f)
			{
				this.ComputeNormal(ref normal);
				this._normal = normal;
				flag = true;
			}
			int num = Vec3.LongAxis(ref normal);
			this._sUnit[num] = 0f;
			this._sUnit[(num + 1) % 3] = this.SUnitX;
			this._sUnit[(num + 2) % 3] = this.SUnitY;
			this._tUnit[num] = 0f;
			this._tUnit[(num + 1) % 3] = ((normal[num] <= 0f) ? this.SUnitY : (-this.SUnitY));
			this._tUnit[(num + 2) % 3] = ((normal[num] <= 0f) ? (-this.SUnitX) : this.SUnitX);
			for (MeshUtils.Vertex next = this._mesh._vHead._next; next != this._mesh._vHead; next = next._next)
			{
				Vec3.Dot(ref next._coords, ref this._sUnit, out next._s);
				Vec3.Dot(ref next._coords, ref this._tUnit, out next._t);
			}
			if (flag)
			{
				this.CheckOrientation();
			}
			bool flag2 = true;
			for (MeshUtils.Vertex next2 = this._mesh._vHead._next; next2 != this._mesh._vHead; next2 = next2._next)
			{
				if (flag2)
				{
					this._bminX = (this._bmaxX = next2._s);
					this._bminY = (this._bmaxY = next2._t);
					flag2 = false;
				}
				else
				{
					if (next2._s < this._bminX)
					{
						this._bminX = next2._s;
					}
					if (next2._s > this._bmaxX)
					{
						this._bmaxX = next2._s;
					}
					if (next2._t < this._bminY)
					{
						this._bminY = next2._t;
					}
					if (next2._t > this._bmaxY)
					{
						this._bmaxY = next2._t;
					}
				}
			}
		}

		private void TessellateMonoRegion(MeshUtils.Face face)
		{
			MeshUtils.Edge edge = face._anEdge;
			while (Geom.VertLeq(edge._Dst, edge._Org))
			{
				edge = edge._Lprev;
			}
			while (Geom.VertLeq(edge._Org, edge._Dst))
			{
				edge = edge._Lnext;
			}
			MeshUtils.Edge edge2 = edge._Lprev;
			while (edge._Lnext != edge2)
			{
				if (Geom.VertLeq(edge._Dst, edge2._Org))
				{
					while (edge2._Lnext != edge && (Geom.EdgeGoesLeft(edge2._Lnext) || Geom.EdgeSign(edge2._Org, edge2._Dst, edge2._Lnext._Dst) <= 0f))
					{
						edge2 = this._mesh.Connect(edge2._Lnext, edge2)._Sym;
					}
					edge2 = edge2._Lprev;
				}
				else
				{
					while (edge2._Lnext != edge && (Geom.EdgeGoesRight(edge._Lprev) || Geom.EdgeSign(edge._Dst, edge._Org, edge._Lprev._Org) >= 0f))
					{
						edge = this._mesh.Connect(edge, edge._Lprev)._Sym;
					}
					edge = edge._Lnext;
				}
			}
			while (edge2._Lnext._Lnext != edge)
			{
				edge2 = this._mesh.Connect(edge2._Lnext, edge2)._Sym;
			}
		}

		private void TessellateInterior()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				if (face._inside)
				{
					this.TessellateMonoRegion(face);
				}
			}
		}

		private void DiscardExterior()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				if (!face._inside)
				{
					this._mesh.ZapFace(face);
				}
			}
		}

		private void SetWindingNumber(int value, bool keepOnlyBoundary)
		{
			MeshUtils.Edge next;
			for (MeshUtils.Edge edge = this._mesh._eHead._next; edge != this._mesh._eHead; edge = next)
			{
				next = edge._next;
				if (edge._Rface._inside != edge._Lface._inside)
				{
					edge._winding = ((!edge._Lface._inside) ? (-value) : value);
				}
				else if (!keepOnlyBoundary)
				{
					edge._winding = 0;
				}
				else
				{
					this._mesh.Delete(edge);
				}
			}
		}

		private int GetNeighbourFace(MeshUtils.Edge edge)
		{
			if (edge._Rface == null)
			{
				return -1;
			}
			if (!edge._Rface._inside)
			{
				return -1;
			}
			return edge._Rface._n;
		}

		private void OutputPolymesh(ElementType elementType, int polySize)
		{
			int num = 0;
			int num2 = 0;
			if (polySize < 3)
			{
				polySize = 3;
			}
			if (polySize > 3)
			{
				this._mesh.MergeConvexFaces(polySize);
			}
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				vertex._n = -1;
			}
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				next._n = -1;
				if (next._inside)
				{
					if (this.NoEmptyPolygons)
					{
						float value = MeshUtils.FaceArea(next);
						if (Math.Abs(value) < 1.401298E-45f)
						{
							goto IL_FC;
						}
					}
					MeshUtils.Edge edge = next._anEdge;
					int num3 = 0;
					do
					{
						MeshUtils.Vertex vertex = edge._Org;
						if (vertex._n == -1)
						{
							vertex._n = num2;
							num2++;
						}
						num3++;
						edge = edge._Lnext;
					}
					while (edge != next._anEdge);
					next._n = num;
					num++;
				}
				IL_FC:;
			}
			this._elementCount = num;
			if (elementType == ElementType.ConnectedPolygons)
			{
				num *= 2;
			}
			this._elements = new int[num * polySize];
			this._vertexCount = num2;
			this._vertices = new ContourVertex[this._vertexCount];
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				if (vertex._n != -1)
				{
					this._vertices[vertex._n].Position = vertex._coords;
					this._vertices[vertex._n].Data = vertex._data;
				}
			}
			int num4 = 0;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					if (this.NoEmptyPolygons)
					{
						float value2 = MeshUtils.FaceArea(next);
						if (Math.Abs(value2) < 1.401298E-45f)
						{
							goto IL_2D1;
						}
					}
					MeshUtils.Edge edge = next._anEdge;
					int num3 = 0;
					do
					{
						MeshUtils.Vertex vertex = edge._Org;
						this._elements[num4++] = vertex._n;
						num3++;
						edge = edge._Lnext;
					}
					while (edge != next._anEdge);
					for (int i = num3; i < polySize; i++)
					{
						this._elements[num4++] = -1;
					}
					if (elementType == ElementType.ConnectedPolygons)
					{
						edge = next._anEdge;
						do
						{
							this._elements[num4++] = this.GetNeighbourFace(edge);
							edge = edge._Lnext;
						}
						while (edge != next._anEdge);
						for (int i = num3; i < polySize; i++)
						{
							this._elements[num4++] = -1;
						}
					}
				}
				IL_2D1:;
			}
		}

		private void OutputContours()
		{
			this._vertexCount = 0;
			this._elementCount = 0;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					MeshUtils.Edge anEdge;
					MeshUtils.Edge edge = anEdge = next._anEdge;
					do
					{
						this._vertexCount++;
						edge = edge._Lnext;
					}
					while (edge != anEdge);
					this._elementCount++;
				}
			}
			this._elements = new int[this._elementCount * 2];
			this._vertices = new ContourVertex[this._vertexCount];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					int num4 = 0;
					MeshUtils.Edge anEdge;
					MeshUtils.Edge edge = anEdge = next._anEdge;
					do
					{
						this._vertices[num].Position = edge._Org._coords;
						this._vertices[num].Data = edge._Org._data;
						num++;
						num4++;
						edge = edge._Lnext;
					}
					while (edge != anEdge);
					this._elements[num2++] = num3;
					this._elements[num2++] = num4;
					num3 += num4;
				}
			}
		}

		private float SignedArea(ContourVertex[] vertices)
		{
			float num = 0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				ContourVertex contourVertex = vertices[i];
				ContourVertex contourVertex2 = vertices[(i + 1) % vertices.Length];
				num += contourVertex.Position.X * contourVertex2.Position.Y;
				num -= contourVertex.Position.Y * contourVertex2.Position.X;
			}
			return 0.5f * num;
		}

		public void AddContour(ContourVertex[] vertices)
		{
			this.AddContour(vertices, ContourOrientation.Original);
		}

		public void AddContour(ContourVertex[] vertices, ContourOrientation forceOrientation)
		{
			if (this._mesh == null)
			{
				this._mesh = new LTMesh();
			}
			bool flag = false;
			if (forceOrientation != ContourOrientation.Original)
			{
				float num = this.SignedArea(vertices);
				flag = ((forceOrientation == ContourOrientation.Clockwise && num < 0f) || (forceOrientation == ContourOrientation.CounterClockwise && num > 0f));
			}
			MeshUtils.Edge edge = null;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (edge == null)
				{
					edge = this._mesh.MakeEdge();
					this._mesh.Splice(edge, edge._Sym);
				}
				else
				{
					this._mesh.SplitEdge(edge);
					edge = edge._Lnext;
				}
				int num2 = (!flag) ? i : (vertices.Length - 1 - i);
				edge._Org._coords = vertices[num2].Position;
				edge._Org._data = vertices[num2].Data;
				edge._winding = 1;
				edge._Sym._winding = -1;
			}
		}

		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize)
		{
			this.Tessellate(windingRule, elementType, polySize, null);
		}

		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize, CombineCallback combineCallback)
		{
			this._normal = Vec3.Zero;
			this._vertices = null;
			this._elements = null;
			this._windingRule = windingRule;
			this._combineCallback = combineCallback;
			if (this._mesh == null)
			{
				return;
			}
			this.ProjectPolygon();
			this.ComputeInterior();
			if (elementType == ElementType.BoundaryContours)
			{
				this.SetWindingNumber(1, true);
			}
			else
			{
				this.TessellateInterior();
			}
			if (elementType == ElementType.BoundaryContours)
			{
				this.OutputContours();
			}
			else
			{
				this.OutputPolymesh(elementType, polySize);
			}
			if (this.UsePooling)
			{
				this._mesh.Free();
			}
			this._mesh = null;
		}

		private LTMesh _mesh;

		private Vec3 _normal;

		private Vec3 _sUnit;

		private Vec3 _tUnit;

		private float _bminX;

		private float _bminY;

		private float _bmaxX;

		private float _bmaxY;

		private WindingRule _windingRule;

		private Dict<Tess.ActiveRegion> _dict;

		private PriorityQueue<MeshUtils.Vertex> _pq;

		private MeshUtils.Vertex _event;

		private CombineCallback _combineCallback;

		private ContourVertex[] _vertices;

		private int _vertexCount;

		private int[] _elements;

		private int _elementCount;

		public float SUnitX = 1f;

		public float SUnitY;

		public float SentinelCoord = 4E+30f;

		public bool NoEmptyPolygons;

		public bool UsePooling;

		[CompilerGenerated]
		private static PriorityHeap<MeshUtils.Vertex>.LessOrEqual _003C_003Ef__mg_0024cache0;

		internal class ActiveRegion
		{
			internal MeshUtils.Edge _eUp;

			internal Dict<Tess.ActiveRegion>.Node _nodeUp;

			internal int _windingNumber;

			internal bool _inside;

			internal bool _sentinel;

			internal bool _dirty;

			internal bool _fixUpperEdge;
		}
	}
}
