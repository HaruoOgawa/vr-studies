﻿using UnityEngine;
using System.Collections;

using Photon.Pun;
using Photon.Realtime;

namespace VRStudies { namespace MultiPlayer {

public class Floor : MonoBehaviourPunCallbacks {

	// Joinイベントをリッスン
	//------------------------------------------------------------------------------------------------------------------------------//
	public override void OnJoinedRoom() {

		// 定期的に床の色を変更する
		StartCoroutine( "ChangeFloorColor" );
	}

	//------------------------------------------------------------------------------------------------------------------------------//
	// ルームプロパティによる共通変数の同期
	//------------------------------------------------------------------------------------------------------------------------------//
	IEnumerator ChangeFloorColor() {

		while(true){
				
			//定期的に部屋の色を変更する

			// 自クライアントがマスタークライアントの場合のみ
			if( PhotonNetwork.IsMasterClient ) {

				// ルームプロパティで送信
				var properties  = new ExitGames.Client.Photon.Hashtable();
				properties.Add( "floorColor", new Vector3( Random.value, Random.value, Random.value ) );
				PhotonNetwork.CurrentRoom.SetCustomProperties( properties );
			}
					
			yield return new WaitForSeconds( 3f );
		}
	}

	public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable changedProperties ){

		// ルームプロパティから床の色を取得
		object value = null;
		if (changedProperties.TryGetValue ("floorColor", out value)) {
			Vector3 color = (Vector3)value;
			this.GetComponent<Renderer> ().material.color = new Color( color.x, color.y, color.z );
		}
	}
}

}}